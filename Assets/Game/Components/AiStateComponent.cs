using System;
using EcsRx.Components;
using Game.Enums;
using UniRx;

namespace Game.Components
{
    public class AiStateComponent : IComponent, IDisposable
    {
        public ReactiveProperty<EAiStates> State { get; set; }

        public AiStateComponent()
        {
            State = new ReactiveProperty<EAiStates>(EAiStates.Idle);
        }

        public void Dispose()
        {
            State?.Dispose();
        }
    }
}