using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(BoxCollider))]
    public class DungeonCell : MonoBehaviour
    {
        [HideInInspector] public BoxCollider TriggerBox;
        public GameObject[] Exits;

        private void Awake()
        {
            TriggerBox = GetComponent<BoxCollider>();
            TriggerBox.isTrigger = true;
        }
    }
}