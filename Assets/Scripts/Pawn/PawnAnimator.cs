using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;

        private Animator _animator;

        [SerializeField] private Transform _eyes;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _body;
        [SerializeField, Range(15f, 360f)] private float _turnAngle = 90f;

        public Transform Eyes => _eyes;
        public Transform Head => _head;
        public Transform Body => _body;
        public float TurnAngle => _turnAngle;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void HandleAnimations()
        {
            _animator.SetBool("IsMoving", _pawn.IsMoving);
            _animator.SetBool("IsGrounded", _pawn.IsGrounded);
            _animator.SetBool("IsCrouched", _pawn.IsCrouched);
            _animator.SetFloat("ForwardVelocity", _pawn.ForwardVelocity);
            _animator.SetFloat("RightVelocity", _pawn.RightVelocity);
            _animator.SetFloat("FallVelocity", _pawn.FallVelocity);
            _animator.SetFloat("TurnVelocity", _pawn.TurnVelocity);
        }
    }
}