using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using UnityEngine;

namespace Game.Blueprints
{
    public class BotBlueprint : IBotBlueprint
    {
        public void Apply(IEntity entity)
        {
            Debug.Log($"{GetType().Name} Apply");
            entity.AddComponent<BotComponent>();
            entity.AddComponent<ViewComponent>();
        }
    }
}