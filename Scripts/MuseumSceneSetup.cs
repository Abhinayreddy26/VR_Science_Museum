using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using VRScienceMuseum.Core;
using VRScienceMuseum.UI;
using VRScienceMuseum.VR;
using VRScienceMuseum.Planet;
using VRScienceMuseum.Environment;
using VRScienceMuseum.Tour;

namespace VRScienceMuseum
{
    /// <summary>
    /// Main setup script that orchestrates all museum components.
    /// This script creates and initializes all other managers and UI components.
    /// </summary>
    public class MuseumSceneSetup : MonoBehaviour
    {
        [Header("Setup Options")]
        [SerializeField] private bool setupOnStart = true;

        private GameObject museumRoot;

        private void Start()
        {
            if (setupOnStart)
            {
                SetupMuseum();
            }
        }

        [ContextMenu("Setup Museum")]
        public void SetupMuseum()
        {
            Debug.Log("========================================");
            Debug.Log("[MuseumSetup] Starting VR Science Museum Setup...");
            Debug.Log("========================================");

            // Clear existing
            ClearExisting();

            // Create root object
            museumRoot = new GameObject("VR Science Museum");

            // Setup in order of dependency
            SetupEventSystem();      // 1. Event system first (for UI to work)
            SetupManagers();         // 2. Game managers
            SetupPlayer();           // 3. Player (creates camera)
            SetupEnvironment();      // 4. Environment
            SetupPlanets();          // 5. Planets
            SetupStarfield();        // 6. Starfield
            SetupAudio();            // 7. Audio
            SetupUI();               // 8. UI (last, after everything)

            // Note: GameManager.Start() will trigger MainMenu state after a brief delay

            Debug.Log("========================================");
            Debug.Log("[MuseumSetup] Setup Complete! Press Play to start.");
            Debug.Log("========================================");
        }

        private void ClearExisting()
        {
            var existing = GameObject.Find("VR Science Museum");
            if (existing != null)
            {
                DestroyImmediate(existing);
            }

            // Clear any existing managers
            var existingManagers = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
            foreach (var m in existingManagers) DestroyImmediate(m.gameObject);

            // Clear any existing event systems
            var existingES = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
            foreach (var es in existingES) DestroyImmediate(es.gameObject);
        }

        private void SetupManagers()
        {
            Debug.Log("[MuseumSetup] Creating managers...");

            // Game Manager
            GameObject gmObj = new GameObject("GameManager");
            gmObj.transform.SetParent(museumRoot.transform);
            gmObj.AddComponent<GameManager>();
            Debug.Log($"[MuseumSetup] GameManager created, Instance: {GameManager.Instance != null}");

            // Audio Manager
            GameObject amObj = new GameObject("AudioManager");
            amObj.transform.SetParent(museumRoot.transform);
            amObj.AddComponent<AudioManager>();
            Debug.Log($"[MuseumSetup] AudioManager created, Instance: {AudioManager.Instance != null}");

            // Planet Manager
            GameObject pmObj = new GameObject("PlanetManager");
            pmObj.transform.SetParent(museumRoot.transform);
            pmObj.AddComponent<PlanetManager>();
            Debug.Log($"[MuseumSetup] PlanetManager created, Instance: {PlanetManager.Instance != null}");

            // Environment Setup
            GameObject envObj = new GameObject("EnvironmentSetup");
            envObj.transform.SetParent(museumRoot.transform);
            envObj.AddComponent<EnvironmentSetup>();
            Debug.Log($"[MuseumSetup] EnvironmentSetup created, Instance: {EnvironmentSetup.Instance != null}");

            // Starfield Generator
            GameObject sfObj = new GameObject("StarfieldGenerator");
            sfObj.transform.SetParent(museumRoot.transform);
            sfObj.AddComponent<StarfieldGenerator>();
            Debug.Log($"[MuseumSetup] StarfieldGenerator created, Instance: {StarfieldGenerator.Instance != null}");

            // Guided Tour Controller
            GameObject gtObj = new GameObject("GuidedTourController");
            gtObj.transform.SetParent(museumRoot.transform);
            gtObj.AddComponent<GuidedTourController>();
            Debug.Log($"[MuseumSetup] GuidedTourController created, Instance: {GuidedTourController.Instance != null}");

            Debug.Log("[MuseumSetup] All managers created successfully");
        }

        private void SetupPlayer()
        {
            Debug.Log("[MuseumSetup] Creating player...");

            GameObject player = new GameObject("Player");
            player.transform.SetParent(museumRoot.transform);
            player.transform.position = new Vector3(0, 1f, 0);

            player.AddComponent<SimplePlayerController>();

            Debug.Log("[MuseumSetup] Player created");
        }

        private void SetupEnvironment()
        {
            Debug.Log("[MuseumSetup] Setting up environment...");

            // Create floor
            EnvironmentSetup.Instance?.CreateFloor(museumRoot.transform);

            // Configure space environment (camera now exists)
            EnvironmentSetup.Instance?.SetupEnvironment();

            Debug.Log("[MuseumSetup] Environment configured");
        }

        private void SetupPlanets()
        {
            Debug.Log("[MuseumSetup] Creating planets...");

            PlanetManager.Instance?.CreatePlanets(museumRoot.transform);
        }

        private void SetupStarfield()
        {
            Debug.Log("[MuseumSetup] Creating starfield...");

            StarfieldGenerator.Instance?.CreateStarfield(museumRoot.transform);
        }

        private void SetupEventSystem()
        {
            Debug.Log("[MuseumSetup] Setting up EventSystem...");

            // Create new EventSystem
            GameObject esObj = new GameObject("EventSystem");
            esObj.AddComponent<EventSystem>();

            // Try new Input System first, fallback to old system
            try
            {
                var inputModule = esObj.AddComponent<InputSystemUIInputModule>();
                Debug.Log("[MuseumSetup] Using New Input System");
            }
            catch (System.Exception)
            {
                // Fallback to old input system
                esObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                Debug.Log("[MuseumSetup] Using Legacy Input System");
            }

            Debug.Log("[MuseumSetup] EventSystem ready");
        }

        private void SetupAudio()
        {
            Debug.Log("[MuseumSetup] Setting up audio...");

            AudioManager.Instance?.Setup(museumRoot.transform);
        }

        private void SetupUI()
        {
            Debug.Log("[MuseumSetup] ========== Creating UI ==========");

            GameObject uiRoot = new GameObject("UI");
            uiRoot.transform.SetParent(museumRoot.transform);

            // Main Menu
            Debug.Log("[MuseumSetup] Creating MainMenuUI...");
            GameObject mainMenuObj = new GameObject("MainMenuController");
            mainMenuObj.transform.SetParent(uiRoot.transform);
            var mainMenu = mainMenuObj.AddComponent<MainMenuUI>();
            mainMenu.CreateUI(uiRoot.transform);
            Debug.Log("[MuseumSetup] MainMenuUI completed");

            // Loading Screen
            GameObject loadingObj = new GameObject("LoadingScreenController");
            loadingObj.transform.SetParent(uiRoot.transform);
            var loading = loadingObj.AddComponent<LoadingScreenUI>();
            loading.CreateUI(uiRoot.transform);

            // HUD
            GameObject hudObj = new GameObject("HUDController");
            hudObj.transform.SetParent(uiRoot.transform);
            var hud = hudObj.AddComponent<HUDController>();
            hud.CreateUI(uiRoot.transform);

            // Pause Menu
            GameObject pauseObj = new GameObject("PauseMenuController");
            pauseObj.transform.SetParent(uiRoot.transform);
            var pause = pauseObj.AddComponent<PauseMenuUI>();
            pause.CreateUI(uiRoot.transform);

            Debug.Log("[MuseumSetup] UI created");
        }

        // Emergency OnGUI backup - disabled since MainMenuUI handles it
        private bool showEmergencyMenu = false; // Disabled - MainMenuUI handles menu
        private string emergencyName = "Explorer";

        private void OnGUI()
        {
            // MainMenuUI now handles the menu - this is just a backup
            // Keep disabled to avoid duplicate menus
        }

#if UNITY_EDITOR
        [ContextMenu("Clear Museum")]
        public void ClearMuseum()
        {
            ClearExisting();
            Debug.Log("[MuseumSetup] Museum cleared");
        }
#endif
    }
}
