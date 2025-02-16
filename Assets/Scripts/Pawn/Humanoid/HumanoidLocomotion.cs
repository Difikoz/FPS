using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidLocomotion : PawnLocomotion
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}