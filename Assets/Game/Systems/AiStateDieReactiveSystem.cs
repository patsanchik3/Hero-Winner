using System;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.ReactiveSystems.Systems;
using Game.Components;
using UniRx;
using UnityEngine;

namespace Game.Systems
{
    public class AiStateDieReactiveSystem : IReactToEntitySystem
    {
        private static readonly int Die = Animator.StringToHash("Die");
        public IGroup Group => new Group(typeof(AiStateComponent));
        
        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            var movementComponent = entity.GetComponent<AiStateComponent>();
            return movementComponent.State.DistinctUntilChanged().Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            Debug.Log($"AiStateDieReactiveSystem Process state = {entity.GetComponent<AiStateComponent>().State}");
            var animator = entity.GetComponent<BotViewComponent>().BotView.GetAnimator;
            animator.SetTrigger(Die);
        }
    }
}