using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRScienceMuseum.Core;

namespace VRScienceMuseum.UI
{
    public class LoadingScreenUI : MonoBehaviour
    {
        public static LoadingScreenUI Instance { get; private set; }

        [SerializeField] private float loadingDuration = 4f;

        private GameObject canvas;
        private Text welcomeText;
        private Image progressFill;
        private Text percentText;
        private Text factText;
        private Text statusText;

        private string[] spaceFacts = new string[]
        {
            "The Sun contains 99.86% of all mass in our Solar System!",
            "A day on Venus is longer than its year!",
            "Jupiter's Great Red Spot is a storm that has lasted over 400 years!",
            "Saturn's rings are made of billions of ice and rock particles!",
            "Neptune has the strongest winds in the Solar System - up to 2,100 km/h!",
            "Mars has the largest volcano in the Solar System - Olympus Mons!",
            "Mercury has no atmosphere, causing extreme temperature swings!",
            "Uranus rotates on its side - it's tilted 98 degrees!",
            "Earth is the only planet not named after a Greek or Roman god!",
            "The Sun's core temperature is about 15 million degrees Celsius!",
            "Light from the Sun takes 8 minutes to reach Earth!",
            "You could fit 1,300 Earths inside Jupiter!",
            "Saturn would float if placed in a giant bathtub!",
            "A year on Neptune lasts 165 Earth years!"
        };

        private string[] loadingMessages = new string[]
        {
            "Initializing solar system...",
            "Positioning planets...",
            "Creating starfield...",
            "Calibrating orbits...",
            "Loading planet data...",
            "Preparing your journey...",
            "Almost ready..."
        };

        private void Awake()
        {
            Instance = this;
            Debug.Log("[LoadingScreenUI] Initialized");
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
            if (newState == GameState.Loading)
            {
                Show();
                StartCoroutine(LoadingSequence());
            }
            else
            {
                Hide();
            }
        }

        public void CreateUI(Transform parent)
        {
            Debug.Log("[LoadingScreenUI] Creating Loading Screen...");

            // Subscribe to events immediately
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnGameStateChanged;
            }

            canvas = UIHelper.CreateScreenCanvas("LoadingCanvas", parent, 99);

            // Background
            GameObject bgPanel = UIHelper.CreatePanel(canvas.transform, "Background", new Color(0.01f, 0.01f, 0.05f, 1f));

            // Animated background effect
            CreateWarpEffect(bgPanel.transform);

            // Title
            UIHelper.CreateText(bgPanel.transform, "LoadingTitle", "PREPARING YOUR JOURNEY",
                new Vector2(0.1f, 0.78f), new Vector2(0.9f, 0.90f),
                48, new Color(0.4f, 0.8f, 1f), FontStyle.Bold);

            // Welcome message
            welcomeText = UIHelper.CreateText(bgPanel.transform, "WelcomeText", "Welcome, Explorer!",
                new Vector2(0.15f, 0.65f), new Vector2(0.85f, 0.75f),
                32, Color.white);

            // Progress bar
            progressFill = UIHelper.CreateProgressBar(bgPanel.transform, "ProgressBar",
                new Vector2(0.2f, 0.52f), new Vector2(0.8f, 0.58f),
                new Color(0.15f, 0.15f, 0.25f),
                new Color(0.2f, 0.8f, 0.4f));

            // Percentage text
            percentText = UIHelper.CreateText(bgPanel.transform, "PercentText", "0%",
                new Vector2(0.4f, 0.44f), new Vector2(0.6f, 0.52f),
                24, Color.white, FontStyle.Bold);

            // Status message
            statusText = UIHelper.CreateText(bgPanel.transform, "StatusText", "Initializing...",
                new Vector2(0.2f, 0.38f), new Vector2(0.8f, 0.44f),
                18, new Color(0.6f, 0.6f, 0.7f));

            // Fact box
            GameObject factBox = new GameObject("FactBox");
            factBox.transform.SetParent(bgPanel.transform, false);
            factBox.layer = 5;

            Image factBoxImg = factBox.AddComponent<Image>();
            factBoxImg.color = new Color(0.08f, 0.08f, 0.18f, 0.9f);

            RectTransform factBoxRect = factBox.GetComponent<RectTransform>();
            factBoxRect.anchorMin = new Vector2(0.15f, 0.12f);
            factBoxRect.anchorMax = new Vector2(0.85f, 0.35f);
            factBoxRect.offsetMin = Vector2.zero;
            factBoxRect.offsetMax = Vector2.zero;

            // Fact header
            UIHelper.CreateText(factBox.transform, "FactHeader", "DID YOU KNOW?",
                new Vector2(0.05f, 0.7f), new Vector2(0.95f, 0.95f),
                20, new Color(1f, 0.9f, 0.3f), FontStyle.Bold);

            // Fact text
            factText = UIHelper.CreateText(factBox.transform, "FactText", spaceFacts[0],
                new Vector2(0.05f, 0.1f), new Vector2(0.95f, 0.7f),
                22, Color.white);

            canvas.SetActive(false);
            Debug.Log("[LoadingScreenUI] Loading Screen created successfully");
        }

        private void CreateWarpEffect(Transform parent)
        {
            // Create moving star lines for warp effect
            for (int i = 0; i < 50; i++)
            {
                GameObject line = new GameObject($"WarpLine_{i}");
                line.transform.SetParent(parent, false);
                line.layer = 5;

                Image lineImg = line.AddComponent<Image>();
                lineImg.color = new Color(0.5f, 0.6f, 1f, Random.Range(0.1f, 0.4f));

                RectTransform lineRect = line.GetComponent<RectTransform>();
                float x = Random.Range(0f, 1f);
                float y = Random.Range(0f, 1f);
                lineRect.anchorMin = new Vector2(x, y);
                lineRect.anchorMax = new Vector2(x, y);
                lineRect.sizeDelta = new Vector2(Random.Range(50f, 200f), 2f);
                lineRect.localRotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));

                line.AddComponent<WarpLineAnimation>();
            }
        }

        private IEnumerator LoadingSequence()
        {
            float elapsed = 0f;
            int factIndex = Random.Range(0, spaceFacts.Length);
            int messageIndex = 0;
            float lastFactChange = 0f;
            float lastMessageChange = 0f;

            // Update welcome text with player name
            string playerName = GameManager.Instance?.PlayerName ?? "Explorer";
            if (welcomeText != null)
            {
                welcomeText.text = $"Welcome, {playerName}!\nYour cosmic journey is about to begin...";
            }

            // Initial fact
            if (factText != null)
            {
                factText.text = spaceFacts[factIndex];
            }

            while (elapsed < loadingDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / loadingDuration;

                // Update progress bar
                if (progressFill != null) progressFill.fillAmount = progress;
                if (percentText != null) percentText.text = $"{Mathf.RoundToInt(progress * 100)}%";

                // Change fact every 1.5 seconds
                if (elapsed - lastFactChange > 1.5f)
                {
                    lastFactChange = elapsed;
                    factIndex = (factIndex + 1) % spaceFacts.Length;
                    if (factText != null) factText.text = spaceFacts[factIndex];
                }

                // Change status message every 0.8 seconds
                if (elapsed - lastMessageChange > 0.8f)
                {
                    lastMessageChange = elapsed;
                    messageIndex = Mathf.Min(messageIndex + 1, loadingMessages.Length - 1);
                    if (statusText != null) statusText.text = loadingMessages[messageIndex];
                }

                yield return null;
            }

            // Complete
            if (progressFill != null) progressFill.fillAmount = 1f;
            if (percentText != null) percentText.text = "100%";
            if (statusText != null) statusText.text = "Ready!";

            yield return new WaitForSeconds(0.5f);

            // Transition to game
            AudioManager.Instance?.PlaySFX("start");

            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.IsGuidedTour)
                    GameManager.Instance.ChangeState(GameState.GuidedTour);
                else
                    GameManager.Instance.ChangeState(GameState.Playing);
            }
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

    // Warp line animation effect
    public class WarpLineAnimation : MonoBehaviour
    {
        private RectTransform rect;
        private float speed;
        private float initialX;

        private void Start()
        {
            rect = GetComponent<RectTransform>();
            speed = Random.Range(0.1f, 0.3f);
            initialX = rect.anchorMin.x;
        }

        private void Update()
        {
            if (rect != null)
            {
                float newX = (initialX + Time.time * speed) % 1.2f - 0.1f;
                rect.anchorMin = new Vector2(newX, rect.anchorMin.y);
                rect.anchorMax = new Vector2(newX, rect.anchorMax.y);
            }
        }
    }
}
