using UnityEngine;

namespace WinterUniverse
{
    public class PlayerStats : HumanoidStats
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }
    }
}