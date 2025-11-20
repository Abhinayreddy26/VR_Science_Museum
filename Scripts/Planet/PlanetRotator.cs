using UnityEngine;

namespace VRScienceMuseum.Planet
{
    public class PlanetRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private Vector3 rotationAxis = Vector3.up;

        public void SetSpeed(float speed)
        {
            rotationSpeed = speed;
        }

        private void Update()
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }
}
