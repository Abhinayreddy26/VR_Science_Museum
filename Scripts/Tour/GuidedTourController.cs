using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRScienceMuseum.Core;
using VRScienceMuseum.Planet;
using VRScienceMuseum.VR;

namespace VRScienceMuseum.Tour
{
    public class GuidedTourController : MonoBehaviour
    {
        public static GuidedTourController Instance { get; private set; }

        [SerializeField] private float moveTime = 2f;
        [SerializeField] private float viewTime = 5f;
        [SerializeField] private float viewDistance = 8f;

        private Coroutine tourCoroutine;
        private int currentPlanetIndex = 0;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[GuidedTourController] Initialized");
        }

        private void Start()
        {
            // Subscribe to game state changes
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnGameStateChanged;
            }
        }

        // Allow late subscription after setup
        public void Initialize()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= OnGameStateChanged; // Prevent duplicate
                GameManager.Instance.OnStateChanged += OnGameStateChanged;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= OnGameStateChanged;
            }
        }

        private void OnGameStateChanged(GameState newState)
        {
            if (newState == GameState.GuidedTour)
            {
                StartTour();
            }
            else if (newState != GameState.Paused)
            {
                StopTour();
            }
        }

        public void StartTour()
        {
            if (tourCoroutine != null) StopCoroutine(tourCoroutine);

            currentPlanetIndex = 0;
            tourCoroutine = StartCoroutine(TourSequence());
            Debug.Log("[GuidedTour] Tour started");
        }

        public void StopTour()
        {
            if (tourCoroutine != null)
            {
                StopCoroutine(tourCoroutine);
                tourCoroutine = null;
            }
            Debug.Log("[GuidedTour] Tour stopped");
        }

        private IEnumerator TourSequence()
        {
            var player = FindFirstObjectByType<SimplePlayerController>();
            if (player == null)
            {
                Debug.LogError("[GuidedTour] Player not found!");
                yield break;
            }

            List<Transform> planets = PlanetManager.Instance?.GetPlanetTransforms();
            if (planets == null || planets.Count == 0)
            {
                Debug.LogError("[GuidedTour] No planets found!");
                yield break;
            }

            Debug.Log($"[GuidedTour] Touring {planets.Count} planets...");

            foreach (Transform planetTransform in planets)
            {
                if (GameManager.Instance?.CurrentState != GameState.GuidedTour)
                {
                    Debug.Log("[GuidedTour] Tour interrupted");
                    yield break;
                }

                // Calculate view position
                Vector3 dirToPlanet = (planetTransform.position - Vector3.zero).normalized;
                Vector3 viewPosition = planetTransform.position - dirToPlanet * viewDistance;
                viewPosition.y = 1f;

                // Move to view position
                yield return StartCoroutine(MovePlayerTo(player, viewPosition, planetTransform.position));

                // Show planet detail panel
                var planetInfo = planetTransform.GetComponent<PlanetInfoDisplay>();
                if (planetInfo != null)
                {
                    planetInfo.ShowInfo(); // Show detailed view
                    AudioManager.Instance?.PlaySFX("select");
                    Debug.Log($"[GuidedTour] Showing info for: {planetInfo.PlanetName}");
                }

                // Wait at planet (longer for reading)
                yield return new WaitForSeconds(viewTime + 2f);

                // Hide detail panel (info board stays visible)
                if (planetInfo != null)
                {
                    planetInfo.HideInfo();
                }

                currentPlanetIndex++;
            }

            Debug.Log("[GuidedTour] Tour complete!");

            // Switch to free explore
            if (GameManager.Instance != null)
            {
                GameManager.Instance.IsGuidedTour = false;
                GameManager.Instance.ChangeState(GameState.Playing);
            }
        }

        private IEnumerator MovePlayerTo(SimplePlayerController player, Vector3 targetPos, Vector3 lookAt)
        {
            float elapsed = 0f;
            Vector3 startPos = player.transform.position;
            Quaternion startRot = player.transform.rotation;

            Vector3 lookDir = lookAt - targetPos;
            lookDir.y = 0;
            Quaternion targetRot = lookDir != Vector3.zero ? Quaternion.LookRotation(lookDir) : startRot;

            while (elapsed < moveTime)
            {
                if (GameManager.Instance?.CurrentState != GameState.GuidedTour)
                    yield break;

                elapsed += Time.deltaTime;
                float t = elapsed / moveTime;
                t = t * t * (3f - 2f * t); // Smooth step

                player.TeleportTo(Vector3.Lerp(startPos, targetPos, t));
                player.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

                yield return null;
            }

            player.TeleportTo(targetPos);
            player.transform.rotation = targetRot;
        }

        public int GetCurrentPlanetIndex() => currentPlanetIndex;
    }
}
