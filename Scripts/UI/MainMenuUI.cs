using UnityEngine;
using UnityEngine.UI;
using VRScienceMuseum.Core;

namespace VRScienceMuseum.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public static MainMenuUI Instance { get; private set; }

        private GameObject canvas;
        private InputField nameInputField;
        private bool useOnGUIFallback = true; // Always use OnGUI for reliability
        private string inputName = "Explorer";
        private GUIStyle titleStyle;
        private GUIStyle buttonStyle;
        private GUIStyle inputStyle;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[MainMenuUI] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= OnGameStateChanged;
            }
        }

        private void Start()
        {
            // Ensure cursor is visible for menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Force show menu at start
            if (canvas != null)
            {
                canvas.SetActive(true);
                Debug.Log("[MainMenuUI] Start - Forced canvas active");
            }
        }

        private void Update()
        {
            // Force show menu when in MainMenu state
            if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameState.MainMenu)
            {
                if (canvas != null && !canvas.activeSelf)
                {
                    canvas.SetActive(true);
                    Debug.Log("[MainMenuUI] Update - Forced canvas active for MainMenu state");
                }

                // Check if canvas failed - use OnGUI fallback
                if (canvas == null)
                {
                    useOnGUIFallback = true;
                }
            }
        }

        // Fallback GUI that ALWAYS works - SCALES TO ANY RESOLUTION
        private void OnGUI()
        {
            // Show menu if: GameManager is null OR state is MainMenu
            if (GameManager.Instance != null)
            {
                GameState state = GameManager.Instance.CurrentState;
                if (state == GameState.Loading || state == GameState.Playing ||
                    state == GameState.Paused || state == GameState.GuidedTour)
                {
                    return;
                }
            }

            // SCALE FACTOR based on screen resolution (reference: 1920x1080)
            float scaleX = Screen.width / 1920f;
            float scaleY = Screen.height / 1080f;
            float scale = Mathf.Min(scaleX, scaleY);
            scale = Mathf.Max(scale, 0.5f); // Minimum scale

            // Scaled sizes
            int titleSize = Mathf.RoundToInt(52 * scale);
            int subtitleSize = Mathf.RoundToInt(22 * scale);
            int buttonSize = Mathf.RoundToInt(28 * scale);
            int labelSize = Mathf.RoundToInt(20 * scale);
            int hintSize = Mathf.RoundToInt(16 * scale);

            float panelWidth = 700 * scale;
            float panelHeight = 520 * scale;
            float panelX = (Screen.width - panelWidth) / 2;
            float panelY = (Screen.height - panelHeight) / 2;

            // Create textures for colored buttons
            Texture2D blueTex = MakeTexture(new Color(0.1f, 0.4f, 0.8f));
            Texture2D greenTex = MakeTexture(new Color(0.1f, 0.6f, 0.2f));
            Texture2D redTex = MakeTexture(new Color(0.7f, 0.15f, 0.15f));
            Texture2D darkTex = MakeTexture(new Color(0.08f, 0.08f, 0.18f));
            Texture2D panelTex = MakeTexture(new Color(0.04f, 0.04f, 0.12f));

            // ===== DRAW BACKGROUND =====
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), darkTex);

            // ===== DRAW PANEL =====
            GUI.DrawTexture(new Rect(panelX, panelY, panelWidth, panelHeight), panelTex);

            // Panel border
            GUI.color = new Color(0.3f, 0.5f, 0.9f, 0.8f);
            GUI.DrawTexture(new Rect(panelX, panelY, panelWidth, 4 * scale), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(panelX, panelY + panelHeight - 4 * scale, panelWidth, 4 * scale), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(panelX, panelY, 4 * scale, panelHeight), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(panelX + panelWidth - 4 * scale, panelY, 4 * scale, panelHeight), Texture2D.whiteTexture);
            GUI.color = Color.white;

            // ===== TITLE =====
            GUIStyle titleStyleLocal = new GUIStyle(GUI.skin.label);
            titleStyleLocal.fontSize = titleSize;
            titleStyleLocal.fontStyle = FontStyle.Bold;
            titleStyleLocal.alignment = TextAnchor.MiddleCenter;
            titleStyleLocal.normal.textColor = new Color(1f, 0.85f, 0.2f);
            titleStyleLocal.clipping = TextClipping.Overflow; // Prevent text clipping
            titleStyleLocal.wordWrap = false;
            GUI.Label(new Rect(panelX, panelY + 45 * scale, panelWidth, 80 * scale), "SOLAR SYSTEM VR MUSEUM", titleStyleLocal);

            // ===== SUBTITLE =====
            GUIStyle subStyleLocal = new GUIStyle(GUI.skin.label);
            subStyleLocal.fontSize = subtitleSize;
            subStyleLocal.alignment = TextAnchor.MiddleCenter;
            subStyleLocal.normal.textColor = new Color(0.6f, 0.75f, 1f);
            subStyleLocal.clipping = TextClipping.Overflow;
            GUI.Label(new Rect(panelX, panelY + 130 * scale, panelWidth, 40 * scale), "Explore the wonders of our Solar System", subStyleLocal);

            // ===== DECORATIVE LINE =====
            GUI.color = new Color(0.4f, 0.6f, 1f, 0.6f);
            GUI.DrawTexture(new Rect(panelX + panelWidth * 0.15f, panelY + 175 * scale, panelWidth * 0.7f, 3 * scale), Texture2D.whiteTexture);
            GUI.color = Color.white;

            // ===== NAME LABEL =====
            GUIStyle labelStyleLocal = new GUIStyle(GUI.skin.label);
            labelStyleLocal.fontSize = labelSize;
            labelStyleLocal.alignment = TextAnchor.MiddleCenter;
            labelStyleLocal.normal.textColor = Color.white;
            GUI.Label(new Rect(panelX, panelY + 200 * scale, panelWidth, 35 * scale), "Enter Your Name:", labelStyleLocal);

            // ===== NAME INPUT =====
            GUIStyle inputStyleLocal = new GUIStyle(GUI.skin.textField);
            inputStyleLocal.fontSize = labelSize;
            inputStyleLocal.alignment = TextAnchor.MiddleCenter;
            inputStyleLocal.normal.textColor = Color.white;
            inputStyleLocal.normal.background = MakeTexture(new Color(0.15f, 0.15f, 0.25f));

            float inputWidth = panelWidth * 0.6f;
            float inputX = panelX + (panelWidth - inputWidth) / 2;
            inputName = GUI.TextField(new Rect(inputX, panelY + 240 * scale, inputWidth, 45 * scale), inputName, inputStyleLocal);

            // ===== BUTTONS =====
            float btnWidth = panelWidth * 0.38f;
            float btnHeight = 65 * scale;
            float btnY = panelY + 320 * scale;
            float btnGap = panelWidth * 0.04f;

            GUIStyle btnStyleLocal = new GUIStyle(GUI.skin.button);
            btnStyleLocal.fontSize = buttonSize;
            btnStyleLocal.fontStyle = FontStyle.Bold;
            btnStyleLocal.normal.textColor = Color.white;
            btnStyleLocal.hover.textColor = Color.yellow;
            btnStyleLocal.active.textColor = Color.yellow;

            // GUIDED TOUR button (Blue)
            btnStyleLocal.normal.background = blueTex;
            btnStyleLocal.hover.background = blueTex;
            btnStyleLocal.active.background = blueTex;
            if (GUI.Button(new Rect(panelX + btnGap, btnY, btnWidth, btnHeight), "GUIDED TOUR", btnStyleLocal))
            {
                OnGUIStartGame(true);
            }

            // FREE EXPLORE button (Green)
            btnStyleLocal.normal.background = greenTex;
            btnStyleLocal.hover.background = greenTex;
            btnStyleLocal.active.background = greenTex;
            if (GUI.Button(new Rect(panelX + panelWidth - btnWidth - btnGap, btnY, btnWidth, btnHeight), "FREE EXPLORE", btnStyleLocal))
            {
                OnGUIStartGame(false);
            }

            // EXIT button (Red)
            btnStyleLocal.normal.background = redTex;
            btnStyleLocal.hover.background = redTex;
            btnStyleLocal.active.background = redTex;
            float exitBtnWidth = panelWidth * 0.35f;
            float exitBtnX = panelX + (panelWidth - exitBtnWidth) / 2;
            if (GUI.Button(new Rect(exitBtnX, panelY + 420 * scale, exitBtnWidth, 55 * scale), "EXIT", btnStyleLocal))
            {
                GameManager.Instance?.ExitGame();
            }

            // ===== CONTROLS HINT =====
            GUIStyle hintStyleLocal = new GUIStyle(GUI.skin.label);
            hintStyleLocal.fontSize = hintSize;
            hintStyleLocal.alignment = TextAnchor.MiddleCenter;
            hintStyleLocal.normal.textColor = new Color(0.5f, 0.55f, 0.7f);
            GUI.Label(new Rect(0, Screen.height - 50 * scale, Screen.width, 40 * scale),
                "Controls: WASD = Move | Mouse = Look | Click = Interact | ESC = Pause", hintStyleLocal);

            // Cleanup textures
            if (Application.isPlaying)
            {
                Destroy(blueTex);
                Destroy(greenTex);
                Destroy(redTex);
                Destroy(darkTex);
                Destroy(panelTex);
            }
        }

        private Texture2D MakeTexture(Color color)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        private void OnGUIStartGame(bool guidedTour)
        {
            string playerName = string.IsNullOrWhiteSpace(inputName) ? "Explorer" : inputName;
            Debug.Log($"[MainMenuUI] OnGUI Starting game - Mode: {(guidedTour ? "Guided Tour" : "Free Explore")}, Player: {playerName}");
            GameManager.Instance?.StartGame(guidedTour, playerName);
        }

        private void OnGameStateChanged(GameState newState)
        {
            Debug.Log($"[MainMenuUI] State changed to: {newState}, canvas null: {canvas == null}");
            if (canvas != null)
            {
                bool shouldShow = (newState == GameState.MainMenu);
                canvas.SetActive(shouldShow);
                Debug.Log($"[MainMenuUI] Canvas active: {shouldShow}");
            }
        }

        public void CreateUI(Transform parent)
        {
            Debug.Log("[MainMenuUI] ========== Creating Main Menu ==========");

            try
            {
                // Subscribe to events immediately
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.OnStateChanged += OnGameStateChanged;
                    Debug.Log("[MainMenuUI] Subscribed to GameManager events");
                }
                else
                {
                    Debug.LogError("[MainMenuUI] GameManager.Instance is NULL!");
                }

                // Screen Space Canvas for menu (always visible)
                canvas = UIHelper.CreateScreenCanvas("MainMenuCanvas", parent, 100);
                Debug.Log($"[MainMenuUI] Canvas created: {canvas != null}");

                // Background panel - dark space color
                GameObject bgPanel = UIHelper.CreatePanel(canvas.transform, "Background", new Color(0.02f, 0.02f, 0.08f, 1f));
                Debug.Log($"[MainMenuUI] Background panel created: {bgPanel != null}");

                // Create animated stars background
                CreateStarsBackground(bgPanel.transform);

                // Main content panel (centered)
                GameObject contentPanel = new GameObject("ContentPanel");
                contentPanel.transform.SetParent(bgPanel.transform, false);
                contentPanel.layer = 5;

                RectTransform contentRect = contentPanel.AddComponent<RectTransform>();
                contentRect.anchorMin = new Vector2(0.2f, 0.1f);
                contentRect.anchorMax = new Vector2(0.8f, 0.9f);
                contentRect.offsetMin = Vector2.zero;
                contentRect.offsetMax = Vector2.zero;

                Image contentBg = contentPanel.AddComponent<Image>();
                contentBg.color = new Color(0.05f, 0.05f, 0.15f, 0.9f);

                // Title
                UIHelper.CreateText(contentPanel.transform, "Title", "SOLAR SYSTEM VR MUSEUM",
                    new Vector2(0.05f, 0.78f), new Vector2(0.95f, 0.95f),
                    56, new Color(1f, 0.9f, 0.3f), FontStyle.Bold);

                // Subtitle
                UIHelper.CreateText(contentPanel.transform, "Subtitle", "Explore the wonders of our Solar System",
                    new Vector2(0.1f, 0.70f), new Vector2(0.9f, 0.78f),
                    24, new Color(0.7f, 0.8f, 1f), FontStyle.Italic);

                // Decorative line
                GameObject line = new GameObject("Line");
                line.transform.SetParent(contentPanel.transform, false);
                line.layer = 5;
                Image lineImg = line.AddComponent<Image>();
                lineImg.color = new Color(0.3f, 0.5f, 0.8f, 0.8f);
                RectTransform lineRect = line.GetComponent<RectTransform>();
                lineRect.anchorMin = new Vector2(0.2f, 0.68f);
                lineRect.anchorMax = new Vector2(0.8f, 0.69f);
                lineRect.offsetMin = Vector2.zero;
                lineRect.offsetMax = Vector2.zero;

                // Name Input Label
                UIHelper.CreateText(contentPanel.transform, "NameLabel", "Enter Your Name:",
                    new Vector2(0.2f, 0.54f), new Vector2(0.8f, 0.62f),
                    22, Color.white);

                Debug.Log("[MainMenuUI] About to create input field...");

                // Name Input Field
                nameInputField = UIHelper.CreateInputField(contentPanel.transform, "NameInput", "",
                    new Vector2(0.2f, 0.44f), new Vector2(0.8f, 0.54f));

                Debug.Log($"[MainMenuUI] Input field created: {nameInputField != null}");

                // Guided Tour Button
                UIHelper.CreateButton(contentPanel.transform, "GuidedTourBtn", "GUIDED TOUR",
                    new Vector2(0.15f, 0.26f), new Vector2(0.48f, 0.40f),
                    new Color(0.1f, 0.5f, 0.9f), () => OnStartGame(true));

                // Free Explore Button
                UIHelper.CreateButton(contentPanel.transform, "FreeExploreBtn", "FREE EXPLORE",
                    new Vector2(0.52f, 0.26f), new Vector2(0.85f, 0.40f),
                    new Color(0.2f, 0.7f, 0.3f), () => OnStartGame(false));

                // Exit Button
                UIHelper.CreateButton(contentPanel.transform, "ExitBtn", "EXIT",
                    new Vector2(0.35f, 0.08f), new Vector2(0.65f, 0.20f),
                    new Color(0.8f, 0.2f, 0.2f), OnExitGame);

                Debug.Log("[MainMenuUI] Buttons created");

                // Controls hint at bottom
                UIHelper.CreateText(bgPanel.transform, "ControlsHint",
                    "Controls: WASD = Move | Mouse = Look | Click = Interact | ESC = Menu",
                    new Vector2(0.1f, 0.02f), new Vector2(0.9f, 0.08f),
                    16, new Color(0.5f, 0.5f, 0.6f));

                // Ensure canvas is visible at start
                canvas.SetActive(true);

                Debug.Log("[MainMenuUI] ========== Main Menu created successfully ==========");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[MainMenuUI] FAILED to create menu: {e.Message}\n{e.StackTrace}");
            }
        }

        private void CreateStarsBackground(Transform parent)
        {
            // Create small star dots as UI elements
            for (int i = 0; i < 100; i++)
            {
                GameObject star = new GameObject($"Star_{i}");
                star.transform.SetParent(parent, false);
                star.layer = 5;

                Image starImg = star.AddComponent<Image>();
                float brightness = Random.Range(0.3f, 1f);
                starImg.color = new Color(brightness, brightness, brightness * 1.1f, brightness);

                RectTransform starRect = star.GetComponent<RectTransform>();
                starRect.anchorMin = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                starRect.anchorMax = starRect.anchorMin;
                float size = Random.Range(2f, 6f);
                starRect.sizeDelta = new Vector2(size, size);

                // Add twinkle animation
                star.AddComponent<StarTwinkle>();
            }
        }

        private void OnStartGame(bool guidedTour)
        {
            string playerName = nameInputField != null ? nameInputField.text : "";
            if (string.IsNullOrWhiteSpace(playerName)) playerName = "Explorer";

            Debug.Log($"[MainMenuUI] Starting game - Mode: {(guidedTour ? "Guided Tour" : "Free Explore")}, Player: {playerName}");

            AudioManager.Instance?.PlaySFX("click");
            GameManager.Instance?.StartGame(guidedTour, playerName);
        }

        private void OnExitGame()
        {
            Debug.Log("[MainMenuUI] Exit clicked");
            AudioManager.Instance?.PlaySFX("click");
            GameManager.Instance?.ExitGame();
        }

        public void Show()
        {
            if (canvas != null) canvas.SetActive(true);
        }

        public void Hide()
        {
            if (canvas != null) canvas.SetActive(false);
        }
    }

    // Simple star twinkle effect
    public class StarTwinkle : MonoBehaviour
    {
        private Image image;
        private float baseAlpha;
        private float speed;
        private float phase;

        private void Start()
        {
            image = GetComponent<Image>();
            if (image != null)
            {
                baseAlpha = image.color.a;
                speed = Random.Range(1f, 3f);
                phase = Random.Range(0f, Mathf.PI * 2f);
            }
        }

        private void Update()
        {
            if (image != null)
            {
                float alpha = baseAlpha * (0.5f + 0.5f * Mathf.Sin(Time.time * speed + phase));
                Color c = image.color;
                c.a = alpha;
                image.color = c;
            }
        }
    }
}
