using UnityEngine;
using VRScienceMuseum.Core;
using System.Collections.Generic;

namespace VRScienceMuseum.Planet
{
    public class PlanetInfoDisplay : MonoBehaviour
    {
        public string PlanetName { get; private set; }
        public bool HasBeenViewed { get; private set; }
        public bool IsShowing => detailPanel != null && detailPanel.activeSelf;

        private string description;
        private PlanetFactData factData;
        private int index;
        private GameObject infoPanel;
        private GameObject detailPanel;

        public void Setup(string name, string desc, int idx)
        {
            PlanetName = name;
            description = desc;
            index = idx;
            factData = GetPlanetFactData(name);
        }

        // COMPREHENSIVE PLANET DATA
        private PlanetFactData GetPlanetFactData(string planetName)
        {
            return planetName.ToLower() switch
            {
                "sun" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Yellow Dwarf Star (G-type)",
                        "Age: 4.6 Billion Years",
                        "Diameter: 1,392,700 km",
                        "Mass: 333,000 x Earth",
                        "Core Temperature: 15 million C"
                    },
                    PhysicalData = new[] {
                        "Surface Temperature: 5,500 C",
                        "Composition: 73% Hydrogen, 25% Helium",
                        "Rotation Period: 25-35 days",
                        "Distance to Earth: 150 million km",
                        "Light reaches Earth: 8 min 20 sec"
                    },
                    InterestingFacts = new[] {
                        "Contains 99.86% of Solar System mass!",
                        "Could fit 1.3 million Earths inside",
                        "Produces energy via nuclear fusion",
                        "Solar wind reaches 900 km per second",
                        "Will become a Red Giant in 5 billion years",
                        "Surface has sunspots and solar flares"
                    }
                },
                "mercury" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Terrestrial (Rocky) Planet",
                        "Position: 1st planet from Sun",
                        "Diameter: 4,879 km",
                        "Mass: 0.055 x Earth",
                        "Moons: 0"
                    },
                    PhysicalData = new[] {
                        "Day Length: 59 Earth days",
                        "Year Length: 88 Earth days",
                        "Distance from Sun: 57.9 million km",
                        "Surface Temp: -180 to 430 C",
                        "Gravity: 38% of Earth"
                    },
                    InterestingFacts = new[] {
                        "Smallest planet in Solar System!",
                        "No atmosphere - no weather",
                        "Extreme temperature swings",
                        "Most cratered planet",
                        "Has ice at its poles despite heat!",
                        "Named after Roman messenger god"
                    }
                },
                "venus" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Terrestrial (Rocky) Planet",
                        "Position: 2nd planet from Sun",
                        "Diameter: 12,104 km",
                        "Mass: 0.82 x Earth",
                        "Moons: 0"
                    },
                    PhysicalData = new[] {
                        "Day Length: 243 Earth days",
                        "Year Length: 225 Earth days",
                        "Distance from Sun: 108.2 million km",
                        "Surface Temp: 465 C (Hottest!)",
                        "Atmosphere: 96% Carbon Dioxide"
                    },
                    InterestingFacts = new[] {
                        "HOTTEST planet in Solar System!",
                        "Rotates BACKWARDS (retrograde)",
                        "Day is longer than its year!",
                        "Called Earth's evil twin",
                        "Thick clouds of sulfuric acid",
                        "Pressure: 90x Earth's surface",
                        "Named after goddess of love"
                    }
                },
                "earth" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Terrestrial (Rocky) Planet",
                        "Position: 3rd planet from Sun",
                        "Diameter: 12,756 km",
                        "Mass: 5.97 x 10^24 kg",
                        "Moons: 1 (The Moon)"
                    },
                    PhysicalData = new[] {
                        "Day Length: 24 hours",
                        "Year Length: 365.25 days",
                        "Distance from Sun: 149.6 million km",
                        "Surface Temp: -89 to 57 C",
                        "Atmosphere: 78% Nitrogen, 21% Oxygen"
                    },
                    InterestingFacts = new[] {
                        "ONLY planet with known LIFE!",
                        "71% covered by water",
                        "Has protective magnetic field",
                        "Tilted 23.5 degrees (causes seasons)",
                        "Atmosphere protects from meteors",
                        "4.5 billion years old",
                        "Humans have lived here 300,000 years"
                    }
                },
                "mars" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Terrestrial (Rocky) Planet",
                        "Position: 4th planet from Sun",
                        "Diameter: 6,792 km",
                        "Mass: 0.11 x Earth",
                        "Moons: 2 (Phobos and Deimos)"
                    },
                    PhysicalData = new[] {
                        "Day Length: 24 hrs 37 min",
                        "Year Length: 687 Earth days",
                        "Distance from Sun: 227.9 million km",
                        "Surface Temp: -125 to 20 C",
                        "Atmosphere: 95% Carbon Dioxide"
                    },
                    InterestingFacts = new[] {
                        "Called the RED PLANET!",
                        "Has Olympus Mons - tallest volcano!",
                        "Olympus Mons is 3x height of Everest",
                        "Has the largest canyon - Valles Marineris",
                        "Evidence of ancient water!",
                        "Target for human colonization",
                        "Has seasonal dust storms",
                        "Named after Roman god of war"
                    }
                },
                "jupiter" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Gas Giant Planet",
                        "Position: 5th planet from Sun",
                        "Diameter: 142,984 km (LARGEST!)",
                        "Mass: 318 x Earth",
                        "Moons: 95 known moons"
                    },
                    PhysicalData = new[] {
                        "Day Length: 10 hours (fastest!)",
                        "Year Length: 12 Earth years",
                        "Distance from Sun: 778.5 million km",
                        "Cloud Temp: -145 C",
                        "Composition: 90% Hydrogen, 10% Helium"
                    },
                    InterestingFacts = new[] {
                        "LARGEST planet - King of Planets!",
                        "Great Red Spot: 400 year old storm!",
                        "Storm is 2x size of Earth",
                        "Could fit 1,300 Earths inside!",
                        "Has faint ring system",
                        "Moon Ganymede larger than Mercury",
                        "Europa may have life in oceans!",
                        "Protects Earth from asteroids",
                        "Named after king of Roman gods"
                    }
                },
                "saturn" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Gas Giant Planet",
                        "Position: 6th planet from Sun",
                        "Diameter: 120,536 km",
                        "Mass: 95 x Earth",
                        "Moons: 146 known moons (MOST!)"
                    },
                    PhysicalData = new[] {
                        "Day Length: 10.7 hours",
                        "Year Length: 29 Earth years",
                        "Distance from Sun: 1.4 billion km",
                        "Cloud Temp: -178 C",
                        "Density: Less than water!"
                    },
                    InterestingFacts = new[] {
                        "FAMOUS FOR BEAUTIFUL RINGS!",
                        "Rings are 90% water ice",
                        "Rings span 282,000 km wide!",
                        "Would float if put in water!",
                        "Moon Titan has thick atmosphere",
                        "Titan has lakes of liquid methane",
                        "Winds reach 1,800 km/h",
                        "Has hexagonal storm at north pole",
                        "Named after Roman god of time"
                    }
                },
                "uranus" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Ice Giant Planet",
                        "Position: 7th planet from Sun",
                        "Diameter: 51,118 km",
                        "Mass: 14.5 x Earth",
                        "Moons: 27 known moons"
                    },
                    PhysicalData = new[] {
                        "Day Length: 17 hours",
                        "Year Length: 84 Earth years",
                        "Distance from Sun: 2.9 billion km",
                        "Cloud Temp: -224 C (Coldest!)",
                        "Composition: Water, Methane, Ammonia ice"
                    },
                    InterestingFacts = new[] {
                        "ROTATES ON ITS SIDE (98 degree tilt)!",
                        "Possibly hit by Earth-sized object",
                        "Has 13 faint rings",
                        "Blue-green color from methane",
                        "COLDEST atmosphere in Solar System",
                        "Moons named after Shakespeare characters",
                        "First planet found by telescope (1781)",
                        "Named after Greek god of the sky"
                    }
                },
                "neptune" => new PlanetFactData
                {
                    BasicInfo = new[] {
                        "Type: Ice Giant Planet",
                        "Position: 8th planet from Sun",
                        "Diameter: 49,528 km",
                        "Mass: 17 x Earth",
                        "Moons: 16 known moons"
                    },
                    PhysicalData = new[] {
                        "Day Length: 16 hours",
                        "Year Length: 165 Earth years",
                        "Distance from Sun: 4.5 billion km",
                        "Cloud Temp: -214 C",
                        "Composition: Water, Methane, Ammonia ice"
                    },
                    InterestingFacts = new[] {
                        "WINDIEST planet - 2,100 km/h winds!",
                        "Has Great Dark Spot (storm)",
                        "Deep blue color from methane",
                        "Found by mathematical prediction!",
                        "Moon Triton orbits backwards",
                        "Triton has geysers of nitrogen",
                        "Takes 165 years to orbit Sun once",
                        "Named after Roman god of the sea"
                    }
                },
                _ => new PlanetFactData
                {
                    BasicInfo = new[] { "Unknown planet" },
                    PhysicalData = new[] { "No data" },
                    InterestingFacts = new[] { "No facts available" }
                }
            };
        }

        private void Start()
        {
            if (!string.IsNullOrEmpty(PlanetName))
            {
                CreateMainInfoBoard();
                CreateDetailedInfoPanel();
            }
        }

        // LARGE, CLEARLY VISIBLE info board - ALWAYS SHOWN
        private void CreateMainInfoBoard()
        {
            if (string.IsNullOrEmpty(PlanetName)) return;

            infoPanel = new GameObject("MainInfoBoard");
            infoPanel.transform.SetParent(transform.parent);

            // Position BELOW the planet for clear visibility
            float planetScale = transform.localScale.x;
            infoPanel.transform.localPosition = new Vector3(0, -planetScale - 2.5f, 0);

            // LARGE background board
            GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Quad);
            bg.name = "Board";
            bg.transform.SetParent(infoPanel.transform);
            bg.transform.localPosition = Vector3.zero;
            bg.transform.localScale = new Vector3(6f, 3.5f, 1f);
            Object.Destroy(bg.GetComponent<Collider>());

            var bgRend = bg.GetComponent<Renderer>();
            Material bgMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            bgMat.color = new Color(0.01f, 0.01f, 0.05f, 0.95f);
            bgRend.material = bgMat;

            // Blue border frame
            CreateBorderFrame(infoPanel.transform, 6f, 3.5f, new Color(0.2f, 0.5f, 1f));

            // LARGE Planet Name Title
            CreateText(infoPanel.transform, "Title", PlanetName.ToUpper(),
                new Vector3(0, 1.3f, -0.02f), 100, 0.028f, new Color(1f, 0.9f, 0.2f), FontStyle.Bold);

            // Position indicator
            CreateText(infoPanel.transform, "Position", $"[ {index + 1} of 9 ]",
                new Vector3(0, 0.9f, -0.02f), 50, 0.015f, new Color(0.5f, 0.7f, 1f), FontStyle.Normal);

            // Main description - LARGE and clear
            CreateText(infoPanel.transform, "Desc", WrapText(description, 50),
                new Vector3(0, 0.4f, -0.02f), 55, 0.018f, Color.white, FontStyle.Normal);

            // Key facts preview (3 most interesting)
            float y = -0.2f;
            for (int i = 0; i < 3 && i < factData.InterestingFacts.Length; i++)
            {
                CreateText(infoPanel.transform, $"Fact{i}", "★ " + factData.InterestingFacts[i],
                    new Vector3(0, y, -0.02f), 45, 0.014f, new Color(0.8f, 1f, 0.8f), FontStyle.Normal);
                y -= 0.35f;
            }

            // "CLICK FOR MORE" prompt
            CreateText(infoPanel.transform, "ClickHint", ">>> CLICK PLANET FOR ALL FACTS <<<",
                new Vector3(0, -1.4f, -0.02f), 50, 0.014f, new Color(1f, 0.7f, 0.3f), FontStyle.Bold);

            infoPanel.AddComponent<FaceCamera>();
            infoPanel.SetActive(true);
        }

        // DETAILED panel with ALL facts - shown on click
        private void CreateDetailedInfoPanel()
        {
            detailPanel = new GameObject("DetailedInfoPanel");
            detailPanel.transform.SetParent(transform.parent);

            // Position in front of player view
            float planetScale = transform.localScale.x;
            detailPanel.transform.localPosition = new Vector3(0, planetScale + 4f, -3f);

            // VERY LARGE background
            GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Quad);
            bg.name = "Background";
            bg.transform.SetParent(detailPanel.transform);
            bg.transform.localPosition = Vector3.zero;
            bg.transform.localScale = new Vector3(12f, 8f, 1f);
            Object.Destroy(bg.GetComponent<Collider>());

            var bgRend = bg.GetComponent<Renderer>();
            Material bgMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            bgMat.color = new Color(0.01f, 0.02f, 0.08f, 0.98f);
            bgRend.material = bgMat;

            // Blue border (matches main info board)
            CreateBorderFrame(detailPanel.transform, 12f, 8f, new Color(0.2f, 0.5f, 1f));

            // Title
            CreateText(detailPanel.transform, "Title", $"=== {PlanetName.ToUpper()} - COMPLETE INFORMATION ===",
                new Vector3(0, 3.5f, -0.02f), 80, 0.025f, new Color(1f, 0.9f, 0.2f), FontStyle.Bold);

            // Three columns of information
            float col1X = -4f;
            float col2X = 0f;
            float col3X = 4f;

            // Column 1: Basic Info
            CreateText(detailPanel.transform, "Col1Header", "BASIC INFO",
                new Vector3(col1X, 2.8f, -0.02f), 60, 0.018f, new Color(0.3f, 0.8f, 1f), FontStyle.Bold);

            float y = 2.3f;
            foreach (string fact in factData.BasicInfo)
            {
                CreateText(detailPanel.transform, "Basic", "• " + fact,
                    new Vector3(col1X, y, -0.02f), 40, 0.012f, Color.white, FontStyle.Normal);
                y -= 0.4f;
            }

            // Column 2: Physical Data
            CreateText(detailPanel.transform, "Col2Header", "PHYSICAL DATA",
                new Vector3(col2X, 2.8f, -0.02f), 60, 0.018f, new Color(0.3f, 1f, 0.8f), FontStyle.Bold);

            y = 2.3f;
            foreach (string fact in factData.PhysicalData)
            {
                CreateText(detailPanel.transform, "Physical", "• " + fact,
                    new Vector3(col2X, y, -0.02f), 40, 0.012f, Color.white, FontStyle.Normal);
                y -= 0.4f;
            }

            // Column 3: Interesting Facts
            CreateText(detailPanel.transform, "Col3Header", "AMAZING FACTS",
                new Vector3(col3X, 2.8f, -0.02f), 60, 0.018f, new Color(1f, 0.8f, 0.3f), FontStyle.Bold);

            y = 2.3f;
            foreach (string fact in factData.InterestingFacts)
            {
                CreateText(detailPanel.transform, "Amazing", "★ " + fact,
                    new Vector3(col3X, y, -0.02f), 40, 0.012f, new Color(1f, 1f, 0.8f), FontStyle.Normal);
                y -= 0.4f;
            }

            // Close instruction
            CreateText(detailPanel.transform, "CloseHint", "[ CLICK PLANET AGAIN TO CLOSE ]",
                new Vector3(0, -3.5f, -0.02f), 55, 0.016f, new Color(0.7f, 0.7f, 0.8f), FontStyle.Italic);

            detailPanel.AddComponent<FaceCamera>();
            detailPanel.SetActive(false);
        }

        private void CreateBorderFrame(Transform parent, float width, float height, Color color)
        {
            Material borderMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            borderMat.color = color;

            float t = 0.1f; // thickness

            CreateQuad(parent, "TopBorder", new Vector3(0, height/2, -0.01f), new Vector3(width + t*2, t, 1f), borderMat);
            CreateQuad(parent, "BottomBorder", new Vector3(0, -height/2, -0.01f), new Vector3(width + t*2, t, 1f), borderMat);
            CreateQuad(parent, "LeftBorder", new Vector3(-width/2, 0, -0.01f), new Vector3(t, height, 1f), borderMat);
            CreateQuad(parent, "RightBorder", new Vector3(width/2, 0, -0.01f), new Vector3(t, height, 1f), borderMat);

            // Corner accents
            float cornerSize = 0.3f;
            CreateQuad(parent, "TL", new Vector3(-width/2 + cornerSize/2, height/2 - cornerSize/2, -0.015f), new Vector3(cornerSize, cornerSize, 1f), borderMat);
            CreateQuad(parent, "TR", new Vector3(width/2 - cornerSize/2, height/2 - cornerSize/2, -0.015f), new Vector3(cornerSize, cornerSize, 1f), borderMat);
            CreateQuad(parent, "BL", new Vector3(-width/2 + cornerSize/2, -height/2 + cornerSize/2, -0.015f), new Vector3(cornerSize, cornerSize, 1f), borderMat);
            CreateQuad(parent, "BR", new Vector3(width/2 - cornerSize/2, -height/2 + cornerSize/2, -0.015f), new Vector3(cornerSize, cornerSize, 1f), borderMat);
        }

        private void CreateQuad(Transform parent, string name, Vector3 pos, Vector3 scale, Material mat)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = name;
            quad.transform.SetParent(parent);
            quad.transform.localPosition = pos;
            quad.transform.localScale = scale;
            Object.Destroy(quad.GetComponent<Collider>());
            quad.GetComponent<Renderer>().material = mat;
        }

        private void CreateText(Transform parent, string name, string text, Vector3 pos,
            int fontSize, float charSize, Color color, FontStyle style)
        {
            GameObject textObj = new GameObject(name);
            textObj.transform.SetParent(parent);
            textObj.transform.localPosition = pos;

            TextMesh textMesh = textObj.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = charSize;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.color = color;
            textMesh.fontStyle = style;
        }

        private string WrapText(string text, int maxChars)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string result = "";
            int count = 0;
            foreach (char c in text)
            {
                result += c;
                count++;
                if (count >= maxChars && c == ' ')
                {
                    result += "\n";
                    count = 0;
                }
            }
            return result;
        }

        private void OnMouseDown()
        {
            ToggleInfo();
        }

        public void ToggleInfo()
        {
            if (detailPanel == null) return;

            bool show = !detailPanel.activeSelf;
            detailPanel.SetActive(show);

            if (show)
            {
                HasBeenViewed = true;
                GameManager.Instance?.VisitPlanet(PlanetName);
                AudioManager.Instance?.PlaySFX("select");
                Debug.Log($"[PlanetInfo] Showing ALL facts for: {PlanetName}");
            }
        }

        public void ShowInfo()
        {
            if (detailPanel != null)
            {
                detailPanel.SetActive(true);
                HasBeenViewed = true;
            }
        }

        public void HideInfo()
        {
            if (detailPanel != null)
            {
                detailPanel.SetActive(false);
            }
        }
    }

    // Data structure for planet facts
    public class PlanetFactData
    {
        public string[] BasicInfo;
        public string[] PhysicalData;
        public string[] InterestingFacts;
    }
}
