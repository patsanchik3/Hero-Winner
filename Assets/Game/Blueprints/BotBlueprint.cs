using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using Game.Enums;

namespace Game.Blueprints
{
    public class BotBlueprint : IBotBlueprint
    {
        private readonly EAiState _startState;

        public BotBlueprint(EAiState startState)
        {
            _startState = startState;
        }

        public void Apply(IEntity entity)
        {
            entity.AddComponent<BotComponent>();
            entity.AddComponent<ViewComponent>();
            entity.AddComponent<AiStateComponent>().State.Value = _startState;
        }
    }
}