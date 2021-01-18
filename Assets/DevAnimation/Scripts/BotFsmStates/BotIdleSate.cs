using Fsm;
using UnityEngine;

namespace BotFsmStates
{
    public class BotIdleSate : IFsmState<BotBehaviour>
    {
        private static readonly int Idle = Animator.StringToHash("Idle");

        public void Enter(BotBehaviour entity)
        {
            entity.GetAnimator.SetTrigger(Idle);
        }

        public void Execute(BotBehaviour entity)
        {
        
        }

        public void Exit(BotBehaviour entity)
        {
        
        }
    }
}