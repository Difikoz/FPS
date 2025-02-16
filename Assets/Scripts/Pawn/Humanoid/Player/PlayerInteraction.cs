using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteraction : HumanoidInteraction
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }

        public override void CheckInteractable()
        {
            //if (Physics.Raycast(_head.position, _head.forward, out _hit, _interactionDistance, _interactableMask))
            //{
            //    if (_hit.transform.TryGetComponent(out InteractableBase interactable))
            //    {
            //        if (interactable != _interactable)
            //        {
            //            _interactable = interactable;
            //        }
            //    }
            //    else
            //    {
            //        _interactable = null;
            //    }
            //}
            //else
            //{
            //    _interactable = null;
            //}
        }
    }
}