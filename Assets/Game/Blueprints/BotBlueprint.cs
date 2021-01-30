using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using Game.Enums;

namespace Game.Blueprints
{
    public class BotBlueprint : IBotBlueprint
    {
        public void Apply(IEntity entity)
        {
            entity.AddComponent<BotComponent>();
            entity.AddComponent<ViewComponent>();
            var aiState = entity.AddComponent<AiStateComponent>();
            aiState.State.Value = EAiStates.Die;
        }
    }
}