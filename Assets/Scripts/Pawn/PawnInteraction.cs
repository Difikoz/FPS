using UnityEngine;

namespace WinterUniverse
{
    public class PawnInteraction : MonoBehaviour
    {
        private PawnController _pawn;

        [SerializeField] protected float _interactionDistance = 1f;

        private RaycastHit _hit;
        private InteractableBase _interactable;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public virtual void CheckInteractable()
        {

        }

        public void AttempToPerformInteraction()
        {
            if (_interactable == null)
            {
                return;
            }
            _interactable.Interact();
            _interactable = null;
        }
    }
}