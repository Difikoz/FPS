using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidStats : PawnStats
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}