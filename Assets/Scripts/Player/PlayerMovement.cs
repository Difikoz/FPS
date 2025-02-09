using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _cc;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private float _jumpTime;
        private float _groundedTime;
        private bool _isGrounded;
        private RaycastHit _groundHit;

        [SerializeField] private float _acceleration = 8f;
        [SerializeField] private float _deceleration = 16f;
        [SerializeField] private float _maxSpeed = 4f;
        [SerializeField] private float _jumpForce = 2f;
        [SerializeField] private float _gravity = -20f;
        [SerializeField] private float _timeToJump = 0.5f;
        [SerializeField] private float _timeToFall = 0.5f;
        [SerializeField] private LayerMask _groundMask;

        public void Initialize()
        {
            _cc = GetComponent<CharacterController>();
        }

        public void Move(Vector2 input)
        {
            if (_jumpTime > 0f)
            {
                _jumpTime -= Time.deltaTime;
                if (_groundedTime > 0f)
                {
                    ApplyJumpForce();
                }
            }
            _isGrounded = _fallVelocity.y <= 0f && Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _groundHit, _cc.center.y - (_cc.radius / 2f), _groundMask);
            if (_isGrounded)
            {
                _groundedTime = _timeToFall;
                _fallVelocity.y = _gravity / 10f;
            }
            else
            {
                _groundedTime -= Time.deltaTime;
                _fallVelocity.y += _gravity * Time.deltaTime;
            }
            _cc.Move(_fallVelocity * Time.deltaTime);
            if (input != Vector2.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, (transform.forward * input.y + transform.right * input.x).normalized * _maxSpeed, _acceleration * Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
            _cc.Move(_moveVelocity * Time.deltaTime);
        }

        public void AttempToPerformJumping()
        {
            _jumpTime = _timeToJump;
        }

        private void ApplyJumpForce()
        {
            _jumpTime = 0f;
            _groundedTime = 0f;
            _fallVelocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }
    }
}