using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        private PawnController _pawn;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }
    }
}