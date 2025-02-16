using UnityEngine;

namespace WinterUniverse
{
    public class PlayerAnimator : HumanoidAnimator
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }
    }
}