using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidInventory : PawnInventory
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}