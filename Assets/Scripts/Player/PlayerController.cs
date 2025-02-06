using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerRotation))]
    public class PlayerController : MonoBehaviour
    {

        private PlayerMovement _movement;
        private PlayerRotation _rotation;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _rotation = GetComponent<PlayerRotation>();
            _movement.Initialize();
        }

        private void Update()
        {
            _movement.HandleMovement(Vector2.zero);
            _rotation.HandleRotation(Vector2.zero);
        }
    }
}