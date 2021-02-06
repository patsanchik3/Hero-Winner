using Game.Systems;
using Game.ViewResolvers;
using Plugins.EcsRxExtensions;

namespace Game.Installers
{
    public class GameEcsInstaller : EcsRxInstaller
    {
        protected override void BindSystems()
        {
            this.BindSystem<BotSpawnSystem>();
            this.BindSystem<BotViewResolver>();
            this.BindSystem<AiStateDieReactiveSystem>();
            this.BindSystem<SelfDestructionSystem>();
        }
    }
}
