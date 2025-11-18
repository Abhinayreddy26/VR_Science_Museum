using UnityEngine;

namespace VRScienceMuseum.Environment
{
    public class EnvironmentSetup : MonoBehaviour
    {
        public static EnvironmentSetup Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Debug.Log("[EnvironmentSetup] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void SetupEnvironment()
        {
            Debug.Log("[EnvironmentSetup] Configuring space environment...");

            // Dark ambient lighting
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.15f);

            // Remove skybox
            RenderSettings.skybox = null;

            // Configure camera
            if (Camera.main != null)
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
                Camera.main.backgroundColor = Color.black;
            }

            Debug.Log("[EnvironmentSetup] Space environment configured");
        }

        public void CreateFloor(Transform parent)
        {
            Debug.Log("[EnvironmentSetup] Creating floor...");

            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.name = "Floor";
            floor.transform.SetParent(parent);
            floor.transform.position = Vector3.zero;
            floor.transform.localScale = new Vector3(50f, 1f, 50f);

            // Make invisible but keep collider
            var renderer = floor.GetComponent<Renderer>();
            renderer.enabled = false;

            Debug.Log("[EnvironmentSetup] Floor created");
        }
    }
}
