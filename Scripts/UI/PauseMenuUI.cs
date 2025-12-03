using UnityEngine;
using UnityEngine.UI;
using VRScienceMuseum.Core;
using VRScienceMuseum.Tour;

namespace VRScienceMuseum.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        public static PauseMenuUI Instance { get; private set; }

        private GameObject canvas;
        private Text modeText;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[PauseMenuUI] Initialized");
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
            if (canvas != null)
            {
                canvas.SetActive(newState == GameState.Paused);
            }

            if (newState == GameState.Paused)
            {
                UpdateModeText();
            }
        }

        public void CreateUI(Transform parent)
        {
            Debug.Log("[PauseMenuUI] Creating Pause Menu...");

            // Subscribe to events immediately
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnGameStateChanged;
            }

            // Screen Space canvas for pause menu
            canvas = UIHelper.CreateScreenCanvas("PauseCanvas", parent, 110);

            // Dark overlay
            GameObject overlay = UIHelper.CreatePanel(canvas.transform, "Overlay", new Color(0, 0, 0, 0.75f));

            // Center panel
            GameObject panel = new GameObject("PausePanel");
            panel.transform.SetParent(overlay.transform, false);
            panel.layer = 5;

            Image panelBg = panel.AddComponent<Image>();
            panelBg.color = new Color(0.05f, 0.05f, 0.15f, 0.95f);

            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.3f, 0.2f);
            panelRect.anchorMax = new Vector2(0.7f, 0.8f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            // Title
            UIHelper.CreateText(panel.transform, "PauseTitle", "PAUSED",
                new Vector2(0.1f, 0.82f), new Vector2(0.9f, 0.95f),
                52, new Color(1f, 0.9f, 0.3f), FontStyle.Bold);

            // Current mode display
            modeText = UIHelper.CreateText(panel.transform, "ModeText", "Mode: Free Explore",
                new Vector2(0.1f, 0.72f), new Vector2(0.9f, 0.80f),
                20, new Color(0.7f, 0.8f, 1f));

            // Resume button
            UIHelper.CreateButton(panel.transform, "ResumeBtn", "RESUME",
                new Vector2(0.15f, 0.52f), new Vector2(0.85f, 0.68f),
                new Color(0.2f, 0.7f, 0.3f), OnResume);

            // Switch Mode button
            UIHelper.CreateButton(panel.transform, "SwitchModeBtn", "SWITCH MODE",
                new Vector2(0.15f, 0.33f), new Vector2(0.85f, 0.49f),
                new Color(0.1f, 0.5f, 0.9f), OnSwitchMode);

            // Main Menu button
            UIHelper.CreateButton(panel.transform, "MainMenuBtn", "MAIN MENU",
                new Vector2(0.15f, 0.14f), new Vector2(0.85f, 0.30f),
                new Color(0.8f, 0.2f, 0.2f), OnMainMenu);

            // Hint
            UIHelper.CreateText(panel.transform, "Hint", "Press ESC to resume",
                new Vector2(0.1f, 0.02f), new Vector2(0.9f, 0.12f),
                16, new Color(0.5f, 0.5f, 0.6f));

            canvas.SetActive(false);
            Debug.Log("[PauseMenuUI] Pause Menu created successfully");
        }

        private void UpdateModeText()
        {
            if (modeText != null && GameManager.Instance != null)
            {
                string mode = GameManager.Instance.IsGuidedTour ? "Guided Tour" : "Free Explore";
                modeText.text = $"Current Mode: {mode}";
            }
        }

        private void OnResume()
        {
            Debug.Log("[PauseMenuUI] Resume clicked");
            AudioManager.Instance?.PlaySFX("click");
            GameManager.Instance?.ResumeGame();
        }

        private void OnSwitchMode()
        {
            Debug.Log("[PauseMenuUI] Switch Mode clicked");
            AudioManager.Instance?.PlaySFX("click");

            // Stop guided tour if switching away from it
            if (GameManager.Instance?.IsGuidedTour == true)
            {
                GuidedTourController.Instance?.StopTour();
            }

            GameManager.Instance?.SwitchMode();
            UpdateModeText();

            // Resume with new mode
            GameManager.Instance?.ResumeGame();

            // Start guided tour if switched to it
            if (GameManager.Instance?.IsGuidedTour == true)
            {
                GuidedTourController.Instance?.StartTour();
            }
        }

        private void OnMainMenu()
        {
            Debug.Log("[PauseMenuUI] Main Menu clicked");
            AudioManager.Instance?.PlaySFX("click");
            GuidedTourController.Instance?.StopTour();
            GameManager.Instance?.ReturnToMainMenu();
        }

        public void Show() => canvas?.SetActive(true);
        public void Hide() => canvas?.SetActive(false);
    }
}
