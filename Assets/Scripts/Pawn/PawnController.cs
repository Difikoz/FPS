using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _pawnAnimator;
        private PawnLocomotion _pawnLocomotion;
        private PawnInteraction _pawnInteraction;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnInteraction PawnInteraction => _pawnInteraction;

        public Vector3 MoveDirection;
        public Vector3 LookDirection;
        public float ForwardVelocity;
        public float RightVelocity;
        public float FallVelocity;
        public float TurnVelocity;
        public float AimAngle;
        public bool IsMoving;
        public bool IsRunning;
        public bool IsGrounded;
        public bool IsCrouched;

        private void Awake()
        {
            GetComponents();
            InitializeComponents();
        }

        protected virtual void GetComponents()
        {
            _pawnAnimator = GetComponent<PawnAnimator>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnInteraction = GetComponent<PawnInteraction>();
        }

        protected virtual void InitializeComponents()
        {
            _pawnAnimator.Initialize();
            _pawnLocomotion.Initialize();
            _pawnInteraction.Initialize();
        }

        protected virtual void Update()
        {
            _pawnLocomotion.HandleLocomotion();
            _pawnAnimator.HandleAnimations();
            _pawnInteraction.CheckInteractable();
        }
    }
}