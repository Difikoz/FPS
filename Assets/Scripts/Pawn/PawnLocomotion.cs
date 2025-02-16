using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;

        private CharacterController _cc;
        private Vector3 _moveVelocity;
        private float _jumpTime;
        private float _groundedTime;
        private RaycastHit _groundHit;

        [SerializeField, Range(1f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(1f, 4f)] private float _deceleration = 4f;
        [SerializeField, Range(1f, 4f)] private float _jumpForce = 2f;
        [SerializeField] private float _gravity = -20f;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToJump = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToFall = 0.5f;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _cc = GetComponent<CharacterController>();
        }

        public void HandleLocomotion()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            if (_jumpTime > 0f)
            {
                _jumpTime -= Time.deltaTime;
                if (_groundedTime > 0f)
                {
                    ApplyJumpForce();
                }
            }
            _pawn.IsGrounded = _pawn.FallVelocity <= 0f && Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _groundHit, _cc.center.y - (_cc.radius / 2f), WorldManager.StaticInstance.LayerManager.WalkableMask);
            if (_pawn.IsGrounded)
            {
                _groundedTime = _timeToFall;
                _pawn.FallVelocity = _gravity / 10f;
            }
            else
            {
                _groundedTime -= Time.deltaTime;
                _pawn.FallVelocity += _gravity * Time.deltaTime;
            }
            _cc.Move(Vector3.up * _pawn.FallVelocity * Time.deltaTime);
            if (_pawn.MoveDirection != Vector3.zero)
            {
                if (_pawn.IsRunning && !_pawn.IsCrouched)
                {
                    _moveVelocity = Vector3.MoveTowards(_moveVelocity, _pawn.MoveDirection * 2f, _acceleration * Time.deltaTime);
                }
                else
                {
                    _moveVelocity = Vector3.MoveTowards(_moveVelocity, _pawn.MoveDirection, _acceleration * Time.deltaTime);
                }
            }
            else if (_moveVelocity != Vector3.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
            _pawn.ForwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
            _pawn.RightVelocity = Vector3.Dot(_moveVelocity, transform.right);
            _pawn.IsMoving = _moveVelocity.magnitude > 0.1f;
            //_cc.Move(_moveVelocity * Time.deltaTime);
        }

        public void AttempToPerformJumping()
        {
            _jumpTime = _timeToJump;
        }

        private void ApplyJumpForce()
        {
            _jumpTime = 0f;
            _groundedTime = 0f;
            _pawn.FallVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }

        private void HandleRotation()
        {
            _pawn.TurnVelocity = Vector3.SignedAngle(transform.forward, _pawn.LookDirection, Vector3.up) / _pawn.PawnAnimator.TurnAngle;
            _pawn.AimAngle = Vector3.SignedAngle(transform.forward, _pawn.LookDirection, transform.right);//???
            if (Mathf.Abs(_pawn.TurnVelocity) < 0.1f)
            {
                _pawn.TurnVelocity = 0f;
            }
            if (_pawn.IsMoving)
            {
                transform.Rotate(Vector3.up * _pawn.TurnVelocity * _rotateSpeed * Time.deltaTime);
            }
        }
    }
}