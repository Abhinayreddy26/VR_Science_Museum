using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using VRScienceMuseum.Core;
using VRScienceMuseum.Planet;

namespace VRScienceMuseum.VR
{
    public class SimplePlayerController : MonoBehaviour
    {
        public static SimplePlayerController Instance { get; private set; }

        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float gravity = 9.81f;

        [Header("Look")]
        [SerializeField] private float mouseSensitivity = 0.15f;

        [Header("Interaction")]
        [SerializeField] private float interactionRange = 50f;

        private Camera mainCamera;
        private CharacterController characterController;
        private float rotationX = 0f;
        private bool cursorLocked = false;

        public Transform HeadTransform => mainCamera?.transform;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[SimplePlayerController] Awake - Instance set");
        }

        private void Start()
        {
            CreateCamera();
            CreateCharacterController();

            // Subscribe to game state changes
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnGameStateChanged;

                // Start disabled if we're in menu state
                GameState currentState = GameManager.Instance.CurrentState;
                bool shouldEnable = (currentState == GameState.Playing || currentState == GameState.GuidedTour);
                enabled = shouldEnable;

                // Don't lock cursor in menu
                if (!shouldEnable)
                {
                    cursorLocked = false;
                }
            }

            Debug.Log("[SimplePlayerController] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= OnGameStateChanged;
            }
        }

        private void OnGameStateChanged(GameState newState)
        {
            // Enable for Playing and GuidedTour (for ESC key and interaction)
            bool shouldEnable = (newState == GameState.Playing || newState == GameState.GuidedTour);
            enabled = shouldEnable;

            if (shouldEnable)
            {
                LockCursor(true);
            }
        }

        private void CreateCamera()
        {
            GameObject camObj = new GameObject("Main Camera");
            camObj.transform.SetParent(transform);
            camObj.transform.localPosition = new Vector3(0, 1.7f, 0);
            camObj.tag = "MainCamera";

            mainCamera = camObj.AddComponent<Camera>();
            mainCamera.nearClipPlane = 0.1f;
            mainCamera.farClipPlane = 200f;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = Color.black;

            // Add URP camera data (required for Unity 6 URP)
            var cameraData = camObj.AddComponent<UniversalAdditionalCameraData>();
            cameraData.renderPostProcessing = true;
            cameraData.renderType = CameraRenderType.Base;

            camObj.AddComponent<AudioListener>();

            Debug.Log("[SimplePlayerController] Camera created with URP data");
        }

        private void CreateCharacterController()
        {
            characterController = gameObject.AddComponent<CharacterController>();
            characterController.height = 1.8f;
            characterController.center = new Vector3(0, 0.9f, 0);
            characterController.radius = 0.3f;
        }

        private void Update()
        {
            // Always check for ESC even if disabled
            HandleCursorToggle();

            // Skip rest if disabled
            if (!enabled) return;

            GameState? currentState = GameManager.Instance?.CurrentState;

            // In Playing mode, handle movement and look
            if (currentState == GameState.Playing)
            {
                // Auto-lock cursor if not locked
                if (!cursorLocked)
                {
                    LockCursor(true);
                }

                HandleMovement();
                HandleLook();
            }

            HandleInteraction();
        }

        private void HandleMovement()
        {
            float h = 0, v = 0;

            // Try New Input System first
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed) v = 1;
                if (keyboard.sKey.isPressed) v = -1;
                if (keyboard.aKey.isPressed) h = -1;
                if (keyboard.dKey.isPressed) h = 1;
            }
            else
            {
                // Fallback to legacy Input
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

            Vector3 move = Vector3.zero;

            if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
            {
                Vector3 forward = mainCamera.transform.forward;
                Vector3 right = mainCamera.transform.right;
                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();

                move = (forward * v + right * h) * moveSpeed;
            }

            // Apply gravity
            move.y = -gravity;

            characterController.Move(move * Time.deltaTime);
        }

        private void HandleLook()
        {
            float deltaX = 0, deltaY = 0;

            // Try New Input System first
            var mouse = Mouse.current;
            if (mouse != null)
            {
                Vector2 delta = mouse.delta.ReadValue();
                deltaX = delta.x;
                deltaY = delta.y;
            }
            else
            {
                // Fallback to legacy Input
                deltaX = Input.GetAxis("Mouse X") * 10f;
                deltaY = Input.GetAxis("Mouse Y") * 10f;
            }

            transform.Rotate(0, deltaX * mouseSensitivity, 0);
            rotationX = Mathf.Clamp(rotationX - deltaY * mouseSensitivity, -89f, 89f);
            mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }

        private void HandleInteraction()
        {
            if (mainCamera == null) return;

            bool clicked = false;

            // Try New Input System first
            var mouse = Mouse.current;
            if (mouse != null)
            {
                clicked = mouse.leftButton.wasPressedThisFrame;
            }
            else
            {
                // Fallback to legacy Input
                clicked = Input.GetMouseButtonDown(0);
            }

            if (clicked)
            {
                Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
                {
                    var planetInfo = hit.collider.GetComponent<PlanetInfoDisplay>();
                    if (planetInfo != null)
                    {
                        planetInfo.ToggleInfo();
                        Debug.Log($"[Player] Clicked on {hit.collider.name}");
                    }
                }
            }
        }

        private void HandleCursorToggle()
        {
            bool escPressed = false;

            // Try New Input System first
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                escPressed = keyboard.escapeKey.wasPressedThisFrame;
            }
            else
            {
                // Fallback to legacy Input
                escPressed = Input.GetKeyDown(KeyCode.Escape);
            }

            if (escPressed)
            {
                // In playing or guided tour state, ESC should pause
                GameState? state = GameManager.Instance?.CurrentState;
                if (state == GameState.Playing || state == GameState.GuidedTour)
                {
                    GameManager.Instance.PauseGame();
                }
            }
        }

        public void LockCursor(bool locked)
        {
            cursorLocked = locked;
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
            Debug.Log($"[Player] Cursor locked: {locked}");
        }

        public void TeleportTo(Vector3 position)
        {
            if (characterController != null)
            {
                characterController.enabled = false;
                transform.position = position;
                characterController.enabled = true;
            }
            else
            {
                transform.position = position;
            }
        }

        public void SetPosition(Vector3 position)
        {
            TeleportTo(position);
        }
    }
}
