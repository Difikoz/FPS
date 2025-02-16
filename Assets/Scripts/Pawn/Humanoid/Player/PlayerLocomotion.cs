using UnityEngine;

namespace WinterUniverse
{
    public class PlayerLocomotion : HumanoidLocomotion
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }
    }
}