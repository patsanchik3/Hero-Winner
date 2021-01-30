using EcsRx.Collections.Database;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Systems;
using Game.Blueprints;
using UnityEngine;

namespace Game.Systems
{
    public class BotSpawnSystem : IManualSystem
    {
        private readonly IEntityDatabase _entityDatabase;
        private readonly IBotBlueprint _botBlueprint;

        public BotSpawnSystem(IEntityDatabase entityDatabase, IBotBlueprint botBlueprint)
        {
            _entityDatabase = entityDatabase;
            _botBlueprint = botBlueprint;
        }

        public IGroup Group => new EmptyGroup();
        
        public void StartSystem(IObservableGroup observableGroup)
        {
            var entityPool = _entityDatabase.GetCollection();
            entityPool.CreateEntity(_botBlueprint);
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            
        }
    }
}