using System;
using System.Collections.Generic;
using System.Linq;
using EcsRx.Collections;
using EcsRx.Collections.Database;
using EcsRx.Events;
using EcsRx.Executor;
using EcsRx.Extensions;
using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Infrastructure.Modules;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Plugins.Batching;
using EcsRx.Plugins.Computeds;
using EcsRx.Plugins.ReactiveSystems;
using EcsRx.Plugins.Views;
using EcsRx.Systems;
using EcsRx.Unity.Modules;
using EcsRx.Zenject.Dependencies;
using Zenject;

namespace Plugins.EcsRxExtensions
{
    public class EcsRxInstaller : MonoInstaller, IEcsRxApplication, IDisposable
    {
        [Inject] private DiContainer _diContainer;
        
        public IDependencyContainer Container { get; private set; }
        public ISystemExecutor SystemExecutor { get; private set; }
        public IEventSystem EventSystem { get; private set; }
        public IEntityDatabase EntityDatabase { get; private set; }
        public IObservableGroupManager ObservableGroupManager { get; private set; }
        public IEnumerable<IEcsRxPlugin> Plugins => _plugins;
        private List<IEcsRxPlugin> _plugins { get; set; } = new List<IEcsRxPlugin>();
        

        public override void InstallBindings()
        {
            Container = new ZenjectDependencyContainer(_diContainer);
            StartApplication();
        }
        
        public void Dispose()
        {
            StopApplication();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void StartApplication()
        {
            LoadModules();
            LoadPlugins();
            SetupPlugins();
            ResolveApplicationDependencies();
            BindSystems();
            StartPluginSystems();
            StartSystems();
        }

        public virtual void StopApplication()
        {
            StopAndUnbindAllSystems();
        }

        /// <summary>
        /// Load any modules that your application needs
        /// </summary>
        /// <remarks>
        /// If you wish to use the default setup call through to base, if you wish to stop the default framework
        /// modules loading then do not call base and register your own internal framework module.
        /// </remarks>
        protected virtual void LoadModules()
        {
            Container.LoadModule<FrameworkModule>();
            Container.LoadModule<UnityOverrideModule>();
        }

        /// <summary>
        /// Load any plugins that your application needs
        /// </summary>
        /// <remarks>It is recommended you just call RegisterPlugin method in here for each plugin you need</remarks>
        protected virtual void LoadPlugins()
        {
            RegisterPlugin(new ViewsPlugin());
            RegisterPlugin(new ReactiveSystemsPlugin());
            RegisterPlugin(new BatchPlugin());
            RegisterPlugin(new ComputedsPlugin());
        }

        protected void RegisterPlugin(IEcsRxPlugin plugin)
        {
            _plugins.Add(plugin);
        }

        protected virtual void SetupPlugins()
        {
            _plugins.ForEachRun(x => x.SetupDependencies(Container));
        }

        /// <summary>
        /// Resolve any dependencies the application needs
        /// </summary>
        /// <remarks>By default it will setup SystemExecutor, EventSystem, EntityCollectionManager</remarks>
        protected virtual void ResolveApplicationDependencies()
        {
            SystemExecutor = Container.Resolve<ISystemExecutor>();
            EventSystem = Container.Resolve<IEventSystem>();
            EntityDatabase = Container.Resolve<IEntityDatabase>();
            ObservableGroupManager = Container.Resolve<IObservableGroupManager>();
        }

        protected virtual void StopAndUnbindAllSystems()
        {
            var allSystems = SystemExecutor.Systems.ToList();
            allSystems.ForEachRun(SystemExecutor.RemoveSystem);
            Container.Unbind<ISystem>();
        }

        protected virtual void StartPluginSystems()
        {
            _plugins.SelectMany(x => x.GetSystemsForRegistration(Container))
                .ForEachRun(x => SystemExecutor.AddSystem(x));
        }

        /// <summary>
        /// Bind any systems that the application will need
        /// </summary>
        /// <remarks>By default will auto bind any systems within application scope</remarks>
        protected virtual void BindSystems()
        {
            this.BindAllSystemsWithinApplicationScope();
        }

        /// <summary>
        /// Start any systems that the application will need
        /// </summary>
        /// <remarks>By default it will auto start any systems which have been bound</remarks>
        protected virtual void StartSystems()
        {
            this.StartAllBoundSystems();
        }
    }
}