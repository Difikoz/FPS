using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidEquipment : PawnEquipment
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}