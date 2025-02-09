using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerRotation))]
    [RequireComponent(typeof(PlayerInteraction))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _movement;
        private PlayerRotation _rotation;
        private PlayerInteraction _interaction;
        private CameraController _camera;
        private Vector2 _moveInput;
        private Vector2 _lookInput;

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _lookInput = value.Get<Vector2>();
        }

        public void OnJump()
        {
            _movement.AttempToPerformJumping();
        }

        public void OnInteract()
        {
            _interaction.AttempToPerformInteraction();
        }

        public void OnAttack()
        {

        }

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _rotation = GetComponent<PlayerRotation>();
            _interaction = GetComponent<PlayerInteraction>();
            _camera = GetComponentInChildren<CameraController>();
            _movement.Initialize();
            _camera.Initialize();
        }

        private void Update()
        {
            _movement.Move(_moveInput);
            _rotation.Rotate(_lookInput);
            _interaction.CheckInteractable();
        }

        private void LateUpdate()
        {
            _camera.OnLateUpdate();
        }
    }
}