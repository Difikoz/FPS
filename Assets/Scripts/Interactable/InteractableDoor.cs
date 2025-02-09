using UnityEngine;

namespace WinterUniverse
{
    public class InteractableDoor : InteractableBase
    {
        private bool _opened;

        public override string GetText()
        {
            return _opened ? "Close" : "Open";
        }

        public override void Interact()
        {

        }
    }
}