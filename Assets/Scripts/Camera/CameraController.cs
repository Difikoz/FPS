using UnityEngine;

namespace WinterUniverse
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _head;

        public void Initialize()
        {
            transform.parent = null;
        }

        public void OnLateUpdate()
        {
            transform.SetPositionAndRotation(_head.position, _head.rotation);
        }
    }
}