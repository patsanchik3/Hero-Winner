using System;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.ReactiveSystems.Systems;
using Game.Components;
using Game.Enums;
using Game.Settings;
using UniRx;

namespace Game.Systems
{
    public class AiStateDieReactiveSystem : IReactToEntitySystem
    {
        private readonly IGameSettings _gameSettings;

        public AiStateDieReactiveSystem(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public IGroup Group => new Group(typeof(AiStateComponent));
        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            var aiStateComponent = entity.GetComponent<AiStateComponent>();
            return aiStateComponent.State.DistinctUntilChanged().Where(x => x == EAiState.Die).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var botView = entity.GetComponent<BotViewComponent>().BotView;
            botView.PlayAnimation(AnimationStates.Die);

            var selfDestructComponent = entity.AddComponent<SelfDestructComponent>();
            selfDestructComponent.Lifetime = _gameSettings.DestroyBotTimeSeconds;
        }
    }
}