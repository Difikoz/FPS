using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : HumanoidController
    {
        private PlayerAnimator _playerAnimator;
        private PlayerLocomotion _playerLocomotion;
        private PlayerInteraction _playerInteraction;

        protected override void GetComponents()
        {
            base.GetComponents();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _playerInteraction = GetComponent<PlayerInteraction>();
        }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _playerAnimator.Initialize();
            _playerLocomotion.Initialize();
            _playerInteraction.Initialize();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void Update()
        {
            MoveDirection = transform.forward * WorldManager.StaticInstance.InputManager.MoveInput.y + transform.right * WorldManager.StaticInstance.InputManager.MoveInput.x;
            LookDirection = WorldManager.StaticInstance.CameraManager.Camera.transform.forward;
            if (WorldManager.StaticInstance.InputManager.JumpInput)
            {
                _playerLocomotion.AttempToPerformJumping();
            }
            base.Update();
        }
    }
}