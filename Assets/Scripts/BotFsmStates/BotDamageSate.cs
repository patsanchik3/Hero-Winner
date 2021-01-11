using Fsm;
using UnityEngine;

namespace BotFsmStates
{
    public class BotDamageSate : IFsmState<BotBehaviour>
    {
        private static readonly int Damage = Animator.StringToHash("Damage");

        public void Enter(BotBehaviour entity)
        {
            entity.GetAnimator.SetTrigger(Damage);
        }

        public void Execute(BotBehaviour entity)
        {
        
        }

        public void Exit(BotBehaviour entity)
        {
        
        }
    }
}