using UnityEngine;

namespace VRScienceMuseum.Planet
{
    public class FaceCamera : MonoBehaviour
    {
        private void Update()
        {
            if (Camera.main != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
