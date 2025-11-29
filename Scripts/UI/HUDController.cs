using UnityEngine;
using UnityEngine.UI;
using VRScienceMuseum.Core;
using VRScienceMuseum.Planet;

namespace VRScienceMuseum.UI
{
    public class HUDController : MonoBehaviour
    {
        public static HUDController Instance { get; private set; }

        private GameObject canvas;
        private Text nameText;
        private Text targetText;
        private Text progressText;
        private Text modeText;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[HUDController] Initialized");
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
            bool showHUD = (newState == GameState.Playing || newState == GameState.GuidedTour);
            if (canvas != null) canvas.SetActive(showHUD);

            if (showHUD)
            {
                UpdateHUDContent();
            }
        }

        private void Update()
        {
            if (GameManager.Instance == null) return;

            GameState state = GameManager.Instance.CurrentState;
            if (state == GameState.Playing || state == GameState.GuidedTour)
            {
                UpdateTargetDisplay();
            }
        }

        public void CreateUI(Transform parent)
        {
            Debug.Log("[HUDController] Creating HUD...");

            // Subscribe to events immediately
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnGameStateChanged;
            }

            // Use Screen Space canvas for HUD (always visible on screen)
            canvas = UIHelper.CreateScreenCanvas("HUDCanvas", parent, 50);

            // Top bar background
            GameObject topBar = new GameObject("TopBar");
            topBar.transform.SetParent(canvas.transform, false);
            topBar.layer = 5;

            Image topBarBg = topBar.AddComponent<Image>();
            topBarBg.color = new Color(0.02f, 0.02f, 0.08f, 0.7f);

            RectTransform topBarRect = topBar.GetComponent<RectTransform>();
            topBarRect.anchorMin = new Vector2(0, 0.93f);
            topBarRect.anchorMax = Vector2.one;
            topBarRect.offsetMin = Vector2.zero;
            topBarRect.offsetMax = Vector2.zero;

            // Player name (top left)
            nameText = UIHelper.CreateText(topBar.transform, "PlayerName", "Welcome, Explorer!",
                new Vector2(0.01f, 0), new Vector2(0.3f, 1),
                22, Color.white, FontStyle.Bold);
            nameText.alignment = TextAnchor.MiddleLeft;

            // Current target (top center)
            targetText = UIHelper.CreateText(topBar.transform, "CurrentTarget", "",
                new Vector2(0.35f, 0), new Vector2(0.65f, 1),
                24, new Color(0.4f, 0.8f, 1f), FontStyle.Bold);
            targetText.alignment = TextAnchor.MiddleCenter;

            // Mode indicator (top right)
            modeText = UIHelper.CreateText(topBar.transform, "ModeIndicator", "FREE EXPLORE",
                new Vector2(0.7f, 0), new Vector2(0.99f, 1),
                20, new Color(0.3f, 0.9f, 0.4f), FontStyle.Bold);
            modeText.alignment = TextAnchor.MiddleRight;

            // Controls hint (bottom left)
            GameObject controlsPanel = new GameObject("ControlsPanel");
            controlsPanel.transform.SetParent(canvas.transform, false);
            controlsPanel.layer = 5;

            Image controlsBg = controlsPanel.AddComponent<Image>();
            controlsBg.color = new Color(0, 0, 0, 0.5f);

            RectTransform controlsRect = controlsPanel.GetComponent<RectTransform>();
            controlsRect.anchorMin = new Vector2(0, 0);
            controlsRect.anchorMax = new Vector2(0.2f, 0.1f);
            controlsRect.offsetMin = new Vector2(10, 10);
            controlsRect.offsetMax = Vector2.zero;

            UIHelper.CreateText(controlsPanel.transform, "ControlsText",
                "WASD: Move\nMouse: Look\nClick: Info\nESC: Pause",
                new Vector2(0.05f, 0.05f), new Vector2(0.95f, 0.95f),
                14, new Color(0.7f, 0.7f, 0.8f));

            // Progress (bottom right)
            GameObject progressPanel = new GameObject("ProgressPanel");
            progressPanel.transform.SetParent(canvas.transform, false);
            progressPanel.layer = 5;

            Image progressBg = progressPanel.AddComponent<Image>();
            progressBg.color = new Color(0, 0, 0, 0.5f);

            RectTransform progressRect = progressPanel.GetComponent<RectTransform>();
            progressRect.anchorMin = new Vector2(0.82f, 0);
            progressRect.anchorMax = new Vector2(1, 0.08f);
            progressRect.offsetMin = new Vector2(0, 10);
            progressRect.offsetMax = new Vector2(-10, 0);

            progressText = UIHelper.CreateText(progressPanel.transform, "Progress", "Planets: 0/9",
                new Vector2(0.05f, 0.1f), new Vector2(0.95f, 0.9f),
                18, new Color(0.3f, 0.9f, 0.4f), FontStyle.Bold);

            // Center crosshair
            GameObject crosshair = new GameObject("Crosshair");
            crosshair.transform.SetParent(canvas.transform, false);
            crosshair.layer = 5;

            Image crossImg = crosshair.AddComponent<Image>();
            crossImg.color = new Color(1, 1, 1, 0.5f);

            RectTransform crossRect = crosshair.GetComponent<RectTransform>();
            crossRect.anchorMin = new Vector2(0.5f, 0.5f);
            crossRect.anchorMax = new Vector2(0.5f, 0.5f);
            crossRect.sizeDelta = new Vector2(4, 4);

            canvas.SetActive(false);
            Debug.Log("[HUDController] HUD created successfully");
        }

        public void UpdateHUDContent()
        {
            if (GameManager.Instance == null) return;

            // Update player name
            if (nameText != null)
            {
                nameText.text = $"Welcome, {GameManager.Instance.PlayerName}!";
            }

            // Update mode
            if (modeText != null)
            {
                bool guided = GameManager.Instance.IsGuidedTour;
                modeText.text = guided ? "GUIDED TOUR" : "FREE EXPLORE";
                modeText.color = guided ? new Color(0.4f, 0.8f, 1f) : new Color(0.3f, 0.9f, 0.4f);
            }

            // Update progress
            if (progressText != null)
            {
                int visited = GameManager.Instance.PlanetsVisited;
                progressText.text = $"Planets: {visited}/9";
            }
        }

        private void UpdateTargetDisplay()
        {
            if (targetText == null || Camera.main == null) return;

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                var planetInfo = hit.collider.GetComponent<PlanetInfoDisplay>();
                if (planetInfo != null)
                {
                    targetText.text = $"Looking at: {planetInfo.PlanetName}";
                    return;
                }
            }
            targetText.text = "";
        }

        public void SetTarget(string name)
        {
            if (targetText != null)
            {
                targetText.text = string.IsNullOrEmpty(name) ? "" : $"Looking at: {name}";
            }
        }

        public void Show() => canvas?.SetActive(true);
        public void Hide() => canvas?.SetActive(false);
    }
}
