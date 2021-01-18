using System;
using NUnit.Framework;
using Zenject;

namespace Tests.Editor.SetUp
{
    public abstract class TestBase
    {
        private DiContainer _container;
        
        [SetUp]
        public virtual void SetUp()
        {
            Init();
            Initialize();
        }

        private void Initialize()
        {
            foreach (var initializable in _container.ResolveAll<IInitializable>())
            {
                initializable.Initialize();
            }
        }

        protected abstract void Install(DiContainer container);
        protected abstract void PostInstall(DiContainer container);
        protected abstract void StopApplication(DiContainer container);

        [TearDown]
        public virtual void TearDown()
        {
            StopApplication(_container);
            
            foreach (var disposable in _container.ResolveAll<IDisposable>())
            {
                disposable.Dispose();
            }
            
            _container.UnbindAll();
        }

        private void Init()
        {
            _container = new DiContainer();
            SignalBusInstaller.Install(_container);
            Install(_container);
            PostInstall(_container);
            _container.Inject(this);
            _container.ResolveRoots();
        }
    }
}