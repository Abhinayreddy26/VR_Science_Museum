using UnityEngine;

namespace VRScienceMuseum.Planet
{
    public class PlanetOrbit : MonoBehaviour
    {
        [SerializeField] private float orbitSpeed = 1f;
        [SerializeField] private Vector3 orbitCenter = Vector3.zero;
        [SerializeField] private float orbitRadius = 20f;

        private float currentAngle;

        public void Setup(float speed, Vector3 center, float radius)
        {
            orbitSpeed = speed;
            orbitCenter = center;
            orbitRadius = radius;

            // Calculate initial angle
            Vector3 dir = transform.position - orbitCenter;
            currentAngle = Mathf.Atan2(dir.x, dir.z);
        }

        private void Start()
        {
            if (orbitRadius <= 0)
            {
                Vector3 dir = transform.position - orbitCenter;
                currentAngle = Mathf.Atan2(dir.x, dir.z);
                orbitRadius = dir.magnitude;
            }
        }

        private void Update()
        {
            if (orbitSpeed <= 0) return; // Sun doesn't orbit

            currentAngle += orbitSpeed * Time.deltaTime * 0.1f;

            float x = Mathf.Sin(currentAngle) * orbitRadius;
            float z = Mathf.Cos(currentAngle) * orbitRadius;

            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}
