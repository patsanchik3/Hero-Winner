using Game.Blueprints;
using Game.Enums;
using Zenject;

namespace Game.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BotBlueprint>().AsSingle().WithArguments(EAiState.Die);
        }
    }
}