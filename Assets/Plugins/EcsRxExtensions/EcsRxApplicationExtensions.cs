using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Systems;

namespace Plugins.EcsRxExtensions
{
    public static class EcsRxApplicationExtensions
    {
        public static void BindSystem<T>(this IEcsRxApplication application) where T : ISystem
        {
            application.Container.Bind<ISystem, T>(x => x.WithName(typeof (T).Name));
        }
    }
}