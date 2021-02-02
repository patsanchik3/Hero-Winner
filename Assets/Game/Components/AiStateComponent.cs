using System;
using EcsRx.Components;
using Game.Enums;
using UniRx;

namespace Game.Components
{
    public class AiStateComponent : IComponent, IDisposable
    {
        public ReactiveProperty<EAiState> State { get; set; }

        public AiStateComponent()
        {
            State = new ReactiveProperty<EAiState>(EAiState.Idle);
        }

        public void Dispose()
        {
            State?.Dispose();
        }
    }
}