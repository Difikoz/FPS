using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidAnimator : PawnAnimator
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}