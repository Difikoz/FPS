using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInventory : HumanoidInventory
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }
    }
}