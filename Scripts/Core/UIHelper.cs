using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace VRScienceMuseum.Core
{
    public static class UIHelper
    {
        /// <summary>
        /// Creates a Screen Space Overlay canvas (always visible, for menus)
        /// </summary>
        public static GameObject CreateScreenCanvas(string name, Transform parent, int sortOrder)
        {
            GameObject canvasObj = new GameObject(name);
            if (parent != null) canvasObj.transform.SetParent(parent);
            canvasObj.layer = 5; // UI layer

            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortOrder;

            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            canvasObj.AddComponent<GraphicRaycaster>();

            return canvasObj;
        }

        /// <summary>
        /// Creates a World Space canvas (for 3D positioned UI like planet info)
        /// </summary>
        public static GameObject CreateWorldCanvas(string name, Transform parent, Vector3 position, float scale = 0.002f)
        {
            GameObject canvasObj = new GameObject(name);
            if (parent != null) canvasObj.transform.SetParent(parent);
            canvasObj.layer = 5;

            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 10;

            canvasObj.AddComponent<GraphicRaycaster>();

            RectTransform rect = canvasObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(800, 600);

            canvasObj.transform.position = position;
            canvasObj.transform.localScale = Vector3.one * scale;

            if (Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
            }

            return canvasObj;
        }

        public static GameObject CreatePanel(Transform parent, string name, Color color)
        {
            GameObject panel = new GameObject(name);
            panel.transform.SetParent(parent, false);
            panel.layer = 5;

            Image img = panel.AddComponent<Image>();
            img.color = color;

            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            return panel;
        }

        public static Text CreateText(Transform parent, string name, string content,
            Vector2 anchorMin, Vector2 anchorMax, int fontSize, Color color, FontStyle style = FontStyle.Normal)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(parent, false);
            obj.layer = 5;

            Text text = obj.AddComponent<Text>();
            text.text = content;
            text.font = GetDefaultFont();
            text.fontSize = fontSize;
            text.color = color;
            text.fontStyle = style;
            text.alignment = TextAnchor.MiddleCenter;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            return text;
        }

        public static Button CreateButton(Transform parent, string name, string text,
            Vector2 anchorMin, Vector2 anchorMax, Color color, UnityAction onClick)
        {
            GameObject btnObj = new GameObject(name);
            btnObj.transform.SetParent(parent, false);
            btnObj.layer = 5;

            Image btnImg = btnObj.AddComponent<Image>();
            btnImg.color = color;
            btnImg.type = Image.Type.Sliced;

            Button btn = btnObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f);
            colors.pressedColor = new Color(0.7f, 0.7f, 0.7f);
            colors.selectedColor = Color.white;
            btn.colors = colors;
            btn.targetGraphic = btnImg;

            RectTransform btnRect = btnObj.GetComponent<RectTransform>();
            btnRect.anchorMin = anchorMin;
            btnRect.anchorMax = anchorMax;
            btnRect.offsetMin = Vector2.zero;
            btnRect.offsetMax = Vector2.zero;

            // Button text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            textObj.layer = 5;

            Text btnText = textObj.AddComponent<Text>();
            btnText.text = text;
            btnText.font = GetDefaultFont();
            btnText.fontSize = 32;
            btnText.color = Color.white;
            btnText.fontStyle = FontStyle.Bold;
            btnText.alignment = TextAnchor.MiddleCenter;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            if (onClick != null)
            {
                btn.onClick.AddListener(onClick);
            }

            return btn;
        }

        public static InputField CreateInputField(Transform parent, string name, string defaultText,
            Vector2 anchorMin, Vector2 anchorMax)
        {
            GameObject inputObj = new GameObject(name);
            inputObj.transform.SetParent(parent, false);
            inputObj.layer = 5;

            Image inputBg = inputObj.AddComponent<Image>();
            inputBg.color = new Color(0.15f, 0.15f, 0.25f, 1f);

            InputField inputField = inputObj.AddComponent<InputField>();

            RectTransform inputRect = inputObj.GetComponent<RectTransform>();
            inputRect.anchorMin = anchorMin;
            inputRect.anchorMax = anchorMax;
            inputRect.offsetMin = Vector2.zero;
            inputRect.offsetMax = Vector2.zero;

            // Text area
            GameObject textArea = new GameObject("TextArea");
            textArea.transform.SetParent(inputObj.transform, false);
            textArea.layer = 5;
            textArea.AddComponent<RectMask2D>();

            // RectMask2D already adds RectTransform, so use GetComponent
            RectTransform textAreaRect = textArea.GetComponent<RectTransform>();
            textAreaRect.anchorMin = Vector2.zero;
            textAreaRect.anchorMax = Vector2.one;
            textAreaRect.offsetMin = new Vector2(10, 5);
            textAreaRect.offsetMax = new Vector2(-10, -5);

            // Input text
            GameObject inputTextObj = new GameObject("Text");
            inputTextObj.transform.SetParent(textArea.transform, false);
            inputTextObj.layer = 5;

            Text inputText = inputTextObj.AddComponent<Text>();
            inputText.font = GetDefaultFont();
            inputText.fontSize = 28;
            inputText.color = Color.white;
            inputText.alignment = TextAnchor.MiddleCenter;
            inputText.supportRichText = false;

            RectTransform inputTextRect = inputTextObj.GetComponent<RectTransform>();
            inputTextRect.anchorMin = Vector2.zero;
            inputTextRect.anchorMax = Vector2.one;
            inputTextRect.offsetMin = Vector2.zero;
            inputTextRect.offsetMax = Vector2.zero;

            // Placeholder
            GameObject placeholderObj = new GameObject("Placeholder");
            placeholderObj.transform.SetParent(textArea.transform, false);
            placeholderObj.layer = 5;

            Text placeholder = placeholderObj.AddComponent<Text>();
            placeholder.font = GetDefaultFont();
            placeholder.fontSize = 28;
            placeholder.color = new Color(0.5f, 0.5f, 0.6f);
            placeholder.alignment = TextAnchor.MiddleCenter;
            placeholder.fontStyle = FontStyle.Italic;
            placeholder.text = "Enter your name...";

            RectTransform placeholderRect = placeholderObj.GetComponent<RectTransform>();
            placeholderRect.anchorMin = Vector2.zero;
            placeholderRect.anchorMax = Vector2.one;
            placeholderRect.offsetMin = Vector2.zero;
            placeholderRect.offsetMax = Vector2.zero;

            inputField.textComponent = inputText;
            inputField.placeholder = placeholder;
            inputField.text = defaultText;
            inputField.caretColor = Color.white;
            inputField.selectionColor = new Color(0.3f, 0.5f, 0.8f, 0.5f);

            return inputField;
        }

        public static Image CreateProgressBar(Transform parent, string name, Vector2 anchorMin, Vector2 anchorMax, Color bgColor, Color fillColor)
        {
            // Background
            GameObject bgObj = new GameObject(name + "_BG");
            bgObj.transform.SetParent(parent, false);
            bgObj.layer = 5;

            Image bgImg = bgObj.AddComponent<Image>();
            bgImg.color = bgColor;

            RectTransform bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.anchorMin = anchorMin;
            bgRect.anchorMax = anchorMax;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;

            // Fill
            GameObject fillObj = new GameObject(name + "_Fill");
            fillObj.transform.SetParent(bgObj.transform, false);
            fillObj.layer = 5;

            Image fillImg = fillObj.AddComponent<Image>();
            fillImg.color = fillColor;
            fillImg.type = Image.Type.Filled;
            fillImg.fillMethod = Image.FillMethod.Horizontal;
            fillImg.fillOrigin = 0;
            fillImg.fillAmount = 0f;

            RectTransform fillRect = fillObj.GetComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;

            return fillImg;
        }

        private static Font cachedFont = null;

        private static Font GetDefaultFont()
        {
            // Use cached font if available
            if (cachedFont != null) return cachedFont;

            // Try multiple font options
            string[] fontNames = new string[]
            {
                "LegacyRuntime.ttf",
                "Arial.ttf",
                "Liberation Sans"
            };

            foreach (string fontName in fontNames)
            {
                try
                {
                    cachedFont = Resources.GetBuiltinResource<Font>(fontName);
                    if (cachedFont != null)
                    {
                        Debug.Log($"[UIHelper] Loaded font: {fontName}");
                        return cachedFont;
                    }
                }
                catch (System.Exception)
                {
                    // Try next font
                }
            }

            // Try system fonts
            string[] systemFonts = new string[] { "Arial", "Segoe UI", "Helvetica", "Verdana" };
            foreach (string sysFont in systemFonts)
            {
                try
                {
                    cachedFont = Font.CreateDynamicFontFromOSFont(sysFont, 14);
                    if (cachedFont != null)
                    {
                        Debug.Log($"[UIHelper] Loaded system font: {sysFont}");
                        return cachedFont;
                    }
                }
                catch (System.Exception)
                {
                    // Try next
                }
            }

            Debug.LogError("[UIHelper] Could not load any font!");
            return null;
        }
    }
}
