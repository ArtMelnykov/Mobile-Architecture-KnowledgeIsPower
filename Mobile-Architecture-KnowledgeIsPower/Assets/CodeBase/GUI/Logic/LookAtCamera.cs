using UnityEngine;

namespace CodeBase.GUI.Logic
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start() => 
            _mainCamera = Camera.main;

        private void Update()
        {
            Quaternion lookRotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + lookRotation * Vector3.back, lookRotation * Vector3.up);
        }
    }
}