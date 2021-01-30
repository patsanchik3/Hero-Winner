using System;
using EcsRx.Collections.Database;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Plugins.ReactiveSystems.Systems;
using EcsRx.Systems;
using Game.Components;
using Game.Settings;
using UniRx;

namespace Game.Systems
{
    public class AiStateDieReactiveSystem : IReactToEntitySystem, IManualSystem
    {
        private readonly IEntityDatabase _entityDatabase;
        private readonly IGameSettings _gameSettings;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public AiStateDieReactiveSystem(IEntityDatabase entityDatabase, IGameSettings gameSettings)
        {
            _entityDatabase = entityDatabase;
            _gameSettings = gameSettings;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            _disposable?.Dispose();
        }

        public IGroup Group => new Group(typeof(AiStateComponent));

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            var aiStateComponent = entity.GetComponent<AiStateComponent>();
            return aiStateComponent.State.DistinctUntilChanged().Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var animator = entity.GetComponent<BotViewComponent>().BotView.GetAnimator;
            animator.SetTrigger(AnimationStates.Die);

            Observable.Timer(TimeSpan.FromSeconds(_gameSettings.DestroyBotTimeSeconds))
                .Subscribe(_ => DestroyEntity(entity))
                .AddTo(_disposable);
        }

        private void DestroyEntity(IEntity entity)
        {
            var entityCollection = _entityDatabase.GetCollectionFor(entity);
            entityCollection.RemoveEntity(entity.Id);
        }
    }
}