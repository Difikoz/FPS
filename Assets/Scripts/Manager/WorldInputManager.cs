using UnityEngine;

namespace WinterUniverse
{
    public class WorldInputManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _jumpInput;
        private bool _interactInput;
        private bool _primaryActionInput;
        private bool _secondaryActionInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;
        public bool JumpInput => _jumpInput;
        public bool InteractInput => _interactInput;
        public bool PrimaryActionInput => _primaryActionInput;
        public bool SecondaryActionInput => _secondaryActionInput;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
        }

        public void OnUpdate()
        {
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            _lookInput = _inputActions.Player.Look.ReadValue<Vector2>();
            _jumpInput = _inputActions.Player.Jump.IsPressed();
            _interactInput = _inputActions.Player.Interact.IsPressed();
            _primaryActionInput = _inputActions.Player.PrimaryAction.IsPressed();
            _secondaryActionInput = _inputActions.Player.SecondaryAction.IsPressed();
        }
    }
}