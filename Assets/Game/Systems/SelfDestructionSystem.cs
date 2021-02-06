using System;
using EcsRx.Collections.Database;
using EcsRx.Collections.Entity;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.ReactiveSystems.Systems;
using Game.Components;
using UniRx;
using UnityEngine;

namespace Game.Systems
{
    public class SelfDestructionSystem : IReactToEntitySystem
    {
        public IGroup Group => new Group(typeof(SelfDestructComponent));
        private readonly IEntityCollection _defaultCollection;

        public SelfDestructionSystem(IEntityDatabase entityDatabase)
        {
            _defaultCollection = entityDatabase.GetCollection();
        }

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            var selfDestructComponent = entity.GetComponent<SelfDestructComponent>();
            return Observable.Timer(TimeSpan.FromSeconds(selfDestructComponent.Lifetime))
                .Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            _defaultCollection.RemoveEntity(entity.Id);
        }
    }
}