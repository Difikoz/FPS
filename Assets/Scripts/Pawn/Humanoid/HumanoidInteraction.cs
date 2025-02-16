using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidInteraction : PawnInteraction
    {
        private HumanoidController _humanoid;

        public override void Initialize()
        {
            base.Initialize();
            _humanoid = GetComponent<HumanoidController>();
        }
    }
}