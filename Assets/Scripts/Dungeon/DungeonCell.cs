using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(BoxCollider))]
    public class DungeonCell : MonoBehaviour
    {
        public BoxCollider TriggerBox;
        public GameObject[] Exits;
    }
}