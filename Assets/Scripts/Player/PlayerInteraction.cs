using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private float _interactionDistance = 1f;
        [SerializeField] private LayerMask _interactableMask;

        private RaycastHit _hit;
        private InteractableBase _interactable;

        public void CheckInteractable()
        {
            if (Physics.Raycast(_head.position, _head.forward, out _hit, _interactionDistance, _interactableMask))
            {
                if (_hit.transform.TryGetComponent(out InteractableBase interactable))
                {
                    if (interactable != _interactable)
                    {
                        _interactable = interactable;
                    }
                }
                else
                {
                    _interactable = null;
                }
            }
            else
            {
                _interactable = null;
            }
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