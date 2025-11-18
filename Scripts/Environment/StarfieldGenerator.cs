using UnityEngine;
using System.Collections.Generic;

namespace VRScienceMuseum.Environment
{
    public class StarfieldGenerator : MonoBehaviour
    {
        public static StarfieldGenerator Instance { get; private set; }

        [SerializeField] private int starCount = 500;
        [SerializeField] private float minDistance = 60f;
        [SerializeField] private float maxDistance = 120f;
        [SerializeField] private float minSize = 0.05f;
        [SerializeField] private float maxSize = 0.25f;

        private GameObject starfieldContainer;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[StarfieldGenerator] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void CreateStarfield(Transform parent)
        {
            Debug.Log("[StarfieldGenerator] Creating starfield...");

            starfieldContainer = new GameObject("Starfield");
            starfieldContainer.transform.SetParent(parent);

            for (int i = 0; i < starCount; i++)
            {
                CreateStar(starfieldContainer.transform);
            }

            // Add twinkle effect
            starfieldContainer.AddComponent<StarfieldTwinkle>();

            Debug.Log($"[StarfieldGenerator] Created {starCount} stars");
        }

        private void CreateStar(Transform parent)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            star.name = "Star";
            star.transform.SetParent(parent);

            // Random position on sphere
            Vector3 randomPos = Random.onUnitSphere * Random.Range(minDistance, maxDistance);
            star.transform.position = randomPos;
            star.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

            // Remove collider
            Destroy(star.GetComponent<Collider>());

            // Material
            var renderer = star.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            // Varied star colors
            float colorVariant = Random.value;
            if (colorVariant < 0.7f)
                mat.color = Color.white;
            else if (colorVariant < 0.85f)
                mat.color = new Color(1f, 0.9f, 0.7f); // Yellow
            else
                mat.color = new Color(0.7f, 0.8f, 1f); // Blue

            renderer.material = mat;
        }
    }

    public class StarfieldTwinkle : MonoBehaviour
    {
        private List<Renderer> validRenderers = new List<Renderer>();
        private List<Color> baseColors = new List<Color>();
        private List<float> phases = new List<float>();
        private List<float> speeds = new List<float>();
        private bool isReady = false;

        private void Start()
        {
            // Wait a frame then initialize
            Invoke(nameof(SafeInitialize), 0.2f);
        }

        private void SafeInitialize()
        {
            try
            {
                validRenderers.Clear();
                baseColors.Clear();
                phases.Clear();
                speeds.Clear();

                Renderer[] allRenderers = GetComponentsInChildren<Renderer>();

                if (allRenderers == null || allRenderers.Length == 0)
                {
                    Debug.Log("[StarfieldTwinkle] No renderers found, disabling");
                    enabled = false;
                    return;
                }

                foreach (Renderer rend in allRenderers)
                {
                    if (rend != null && rend.material != null)
                    {
                        validRenderers.Add(rend);
                        baseColors.Add(rend.material.color);
                        phases.Add(Random.Range(0f, Mathf.PI * 2f));
                        speeds.Add(Random.Range(0.5f, 2f));
                    }
                }

                isReady = validRenderers.Count > 0;
                Debug.Log($"[StarfieldTwinkle] Ready with {validRenderers.Count} stars");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[StarfieldTwinkle] Init error: {e.Message}");
                enabled = false;
            }
        }

        private void Update()
        {
            if (!isReady) return;

            try
            {
                for (int i = 0; i < validRenderers.Count; i++)
                {
                    Renderer rend = validRenderers[i];
                    if (rend == null || rend.material == null) continue;

                    // Twinkle between 40% and 100% brightness
                    float intensity = 0.4f + Mathf.Sin(Time.time * speeds[i] + phases[i]) * 0.3f + 0.3f;
                    Color baseColor = baseColors[i];

                    rend.material.color = new Color(
                        baseColor.r * intensity,
                        baseColor.g * intensity,
                        baseColor.b * intensity
                    );
                }
            }
            catch (System.Exception)
            {
                // Silently handle any errors during twinkle
            }
        }
    }
}
