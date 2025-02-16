using UnityEngine;

namespace WinterUniverse
{
    public class PlayerEquipment : HumanoidEquipment
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }
    }
}