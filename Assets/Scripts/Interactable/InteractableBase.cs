using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        public abstract string GetText();
        public abstract void Interact();
    }
}