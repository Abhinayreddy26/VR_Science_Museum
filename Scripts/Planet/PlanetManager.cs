using UnityEngine;
using System.Collections.Generic;

namespace VRScienceMuseum.Planet
{
    public class PlanetManager : MonoBehaviour
    {
        public static PlanetManager Instance { get; private set; }

        [SerializeField] private float orbitRadius = 20f;

        private List<Transform> planetTransforms = new List<Transform>();
        private List<PlanetInfoDisplay> planetInfos = new List<PlanetInfoDisplay>();

        private void Awake()
        {
            Instance = this;
            Debug.Log("[PlanetManager] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void CreatePlanets(Transform parent)
        {
            Debug.Log("[PlanetManager] Creating planets...");

            GameObject container = new GameObject("Planets");
            container.transform.SetParent(parent);

            planetTransforms.Clear();
            planetInfos.Clear();

            PlanetSetupData[] planets = GetPlanetData();

            // Arrange planets in a LINE from left to right (in order: Sun -> Neptune)
            float startX = -35f;  // Start position
            float spacing = 9f;   // Space between planets

            for (int i = 0; i < planets.Length; i++)
            {
                // Position planets in a straight line, ordered left to right
                Vector3 pos = new Vector3(
                    startX + (i * spacing),  // X position based on order
                    2f,                       // Height
                    15f                       // Z position (in front of player)
                );

                GameObject exhibit = CreatePlanetExhibit(container.transform, planets[i], pos, i);
                Transform planetTransform = exhibit.transform.Find(planets[i].name);
                if (planetTransform != null)
                {
                    planetTransforms.Add(planetTransform);
                    var info = planetTransform.GetComponent<PlanetInfoDisplay>();
                    if (info != null) planetInfos.Add(info);
                }
            }

            Debug.Log($"[PlanetManager] Created {planets.Length} planets in order");
        }

        private PlanetSetupData[] GetPlanetData()
        {
            return new PlanetSetupData[]
            {
                new PlanetSetupData("Sun", 3f, new Color(1f, 0.8f, 0.3f),
                    "The Sun is a star at the center of our Solar System. Its diameter is about 1.4 million km.",
                    0f, true),
                new PlanetSetupData("Mercury", 0.4f, new Color(0.6f, 0.6f, 0.6f),
                    "Mercury is the smallest planet and closest to the Sun. A year lasts only 88 Earth days.",
                    4.15f),
                new PlanetSetupData("Venus", 0.6f, new Color(0.9f, 0.7f, 0.4f),
                    "Venus is the hottest planet with surface temperatures of 465 degrees C. It rotates backwards.",
                    1.62f),
                new PlanetSetupData("Earth", 0.65f, new Color(0.2f, 0.5f, 0.9f),
                    "Earth is our home planet. It is the only planet known to support life.",
                    1f),
                new PlanetSetupData("Mars", 0.5f, new Color(0.8f, 0.3f, 0.2f),
                    "Mars is the Red Planet. It has the largest volcano in the solar system - Olympus Mons.",
                    0.53f),
                new PlanetSetupData("Jupiter", 1.8f, new Color(0.8f, 0.7f, 0.5f),
                    "Jupiter is the largest planet. Its Great Red Spot is a storm that has lasted for centuries.",
                    0.084f),
                new PlanetSetupData("Saturn", 1.5f, new Color(0.9f, 0.8f, 0.6f),
                    "Saturn is famous for its beautiful rings made of ice and rock particles.",
                    0.034f),
                new PlanetSetupData("Uranus", 1f, new Color(0.5f, 0.8f, 0.9f),
                    "Uranus rotates on its side. It is an ice giant with 27 known moons.",
                    0.012f),
                new PlanetSetupData("Neptune", 0.95f, new Color(0.3f, 0.4f, 0.9f),
                    "Neptune is the windiest planet with speeds up to 2,100 km/h. It has 14 known moons.",
                    0.006f)
            };
        }

        private GameObject CreatePlanetExhibit(Transform parent, PlanetSetupData data, Vector3 position, int index)
        {
            // Exhibit container - STATIONARY (no orbit)
            GameObject exhibit = new GameObject($"Exhibit_{data.name}");
            exhibit.transform.SetParent(parent);
            exhibit.transform.position = position;

            // NO orbit component - planets stay in place

            // Planet sphere
            GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planet.name = data.name;
            planet.transform.SetParent(exhibit.transform);
            planet.transform.localPosition = Vector3.zero;
            planet.transform.localScale = Vector3.one * data.scale;

            // Material
            ApplyPlanetMaterial(planet, data);

            // Rotation
            planet.AddComponent<PlanetRotator>();

            // Info display
            var info = planet.AddComponent<PlanetInfoDisplay>();
            info.Setup(data.name, data.description, index);

            // Saturn rings
            if (data.name == "Saturn")
            {
                CreateRings(planet.transform);
            }

            // Light
            CreateSpotlight(exhibit.transform, planet.transform);

            // Label
            CreateLabel(exhibit.transform, data.name, -data.scale - 0.5f);

            return exhibit;
        }

        private void ApplyPlanetMaterial(GameObject planet, PlanetSetupData data)
        {
            var renderer = planet.GetComponent<Renderer>();

            // Use Unlit shader for consistent look
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            // Try HIGH QUALITY textures first (4K/2K PNG), then fallback to JPG
            string[] texturePaths = data.name.ToLower() switch
            {
                "sun" => new[] { "VRScienceMuseum/Textures/Planets/2k_sun" },
                "mercury" => new[] { "VRScienceMuseum/Textures/Planets/Mercury_2k", "VRScienceMuseum/Textures/Planets/2k_mercury" },
                "venus" => new[] { "VRScienceMuseum/Textures/Planets/Venus_2K", "VRScienceMuseum/Textures/Planets/2k_venus_surface" },
                "earth" => new[] { "VRScienceMuseum/Textures/Planets/Earth_4k", "VRScienceMuseum/Textures/Planets/2k_earth_daymap" },
                "mars" => new[] { "VRScienceMuseum/Textures/Planets/Mars_2k_hq", "VRScienceMuseum/Textures/Planets/2k_mars" },
                "jupiter" => new[] { "VRScienceMuseum/Textures/Planets/Jupiter_2k", "VRScienceMuseum/Textures/Planets/2k_jupiter" },
                "saturn" => new[] { "VRScienceMuseum/Textures/Planets/Saturn_2k", "VRScienceMuseum/Textures/Planets/2k_saturn" },
                "uranus" => new[] { "VRScienceMuseum/Textures/Planets/Uranus_2k", "VRScienceMuseum/Textures/Planets/2k_uranus" },
                "neptune" => new[] { "VRScienceMuseum/Textures/Planets/Neptune_2k", "VRScienceMuseum/Textures/Planets/2k_neptune" },
                _ => new[] { $"VRScienceMuseum/Textures/Planets/2k_{data.name.ToLower()}" }
            };

            Texture2D tex = null;
            string loadedPath = "";

            // Try each texture path until one works
            foreach (string path in texturePaths)
            {
                tex = Resources.Load<Texture2D>(path);
                if (tex != null)
                {
                    loadedPath = path;
                    break;
                }
            }

            if (tex != null)
            {
                mat.mainTexture = tex;
                mat.color = Color.white; // Show texture properly
                Debug.Log($"[PlanetManager] Loaded HQ texture for {data.name}: {loadedPath} ({tex.width}x{tex.height})");
            }
            else
            {
                // Use nice color if no texture
                mat.color = data.color;
                Debug.Log($"[PlanetManager] No texture found for {data.name}, using color");
            }

            // Make Sun brighter/glow effect
            if (data.isEmissive)
            {
                mat.color = Color.white * 1.5f; // Bright sun
            }

            renderer.material = mat;
        }

        private void CreateRings(Transform planet)
        {
            GameObject rings = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            rings.name = "Rings";
            rings.transform.SetParent(planet);
            rings.transform.localPosition = Vector3.zero;
            rings.transform.localScale = new Vector3(2.5f, 0.01f, 2.5f);

            Object.Destroy(rings.GetComponent<Collider>());

            var renderer = rings.GetComponent<Renderer>();
            Material ringMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            // Try to load ring texture
            Texture2D ringTex = Resources.Load<Texture2D>("VRScienceMuseum/Textures/Planets/Saturn_Ring");
            if (ringTex != null)
            {
                ringMat.mainTexture = ringTex;
                ringMat.color = Color.white;
                Debug.Log("[PlanetManager] Loaded Saturn ring texture");
            }
            else
            {
                ringMat.color = new Color(0.85f, 0.75f, 0.6f, 0.7f);
            }

            renderer.material = ringMat;
        }

        private void CreateSpotlight(Transform exhibit, Transform planet)
        {
            GameObject lightObj = new GameObject("Spotlight");
            lightObj.transform.SetParent(exhibit);
            lightObj.transform.localPosition = new Vector3(0, 5f, 0);

            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Spot;
            light.intensity = 1.5f;
            light.range = 15f;
            light.spotAngle = 50f;
            light.color = Color.white;

            lightObj.transform.LookAt(planet);
        }

        private void CreateLabel(Transform parent, string text, float yOffset)
        {
            GameObject labelObj = new GameObject("Label");
            labelObj.transform.SetParent(parent);
            labelObj.transform.localPosition = new Vector3(0, yOffset, 0);

            TextMesh textMesh = labelObj.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = 50;
            textMesh.characterSize = 0.1f;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.color = Color.white;

            labelObj.AddComponent<FaceCamera>();
        }

        public List<Transform> GetPlanetTransforms() => planetTransforms;
        public int GetTotalCount() => planetTransforms.Count;

        public int GetVisitedCount()
        {
            int count = 0;
            foreach (var info in planetInfos)
            {
                if (info != null && info.HasBeenViewed) count++;
            }
            return count;
        }
    }

    // Planet setup data class
    public class PlanetSetupData
    {
        public string name;
        public float scale;
        public Color color;
        public string description;
        public float orbitSpeed;
        public bool isEmissive;

        public PlanetSetupData(string name, float scale, Color color, string description, float orbitSpeed = 1f, bool isEmissive = false)
        {
            this.name = name;
            this.scale = scale;
            this.color = color;
            this.description = description;
            this.orbitSpeed = orbitSpeed;
            this.isEmissive = isEmissive;
        }
    }
}
