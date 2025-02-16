using UnityEngine;

namespace WinterUniverse
{
    public class WorldLayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _walkableMask;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private LayerMask _interactableMask;

        public LayerMask WalkableMask => _walkableMask;
        public LayerMask ObstacleMask => _obstacleMask;
        public LayerMask InteractableMask => _interactableMask;
    }
}