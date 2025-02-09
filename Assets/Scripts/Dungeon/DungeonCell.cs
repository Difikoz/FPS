using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(BoxCollider))]
    public class DungeonCell : MonoBehaviour
    {
        [Range(1, 100)] public int Chance = 50;
        public BoxCollider TriggerBox;
        public GameObject[] Exits;
    }
}