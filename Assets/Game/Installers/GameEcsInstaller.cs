using Game.Systems;
using Game.ViewResolvers;
using Plugins.EcsRxExtensions;
using Zenject;

namespace Game.Installers
{
    public class GameEcsInstaller : EcsRxInstaller
    {
        protected override void BindSystems()
        {
            this.BindSystem<BotSpawnSystem>();
            this.BindSystem<BotViewResolver>();
        }
    }
}
