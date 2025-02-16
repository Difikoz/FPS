using UnityEngine;

namespace WinterUniverse
{
    public class HumanoidController : PawnController
    {
        private HumanoidAnimator _humanoidAnimator;
        private HumanoidLocomotion _humanoidLocomotion;
        private HumanoidInteraction _humanoidInteraction;

        protected override void GetComponents()
        {
            base.GetComponents();
            _humanoidAnimator = GetComponent<HumanoidAnimator>();
            _humanoidLocomotion = GetComponent<HumanoidLocomotion>();
            _humanoidInteraction = GetComponent<HumanoidInteraction>();
        }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _humanoidAnimator.Initialize();
            _humanoidLocomotion.Initialize();
            _humanoidInteraction.Initialize();
        }
    }
}