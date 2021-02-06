using EcsRx.Collections.Database;
using EcsRx.Extensions;
using Game.Components;
using Game.Systems;
using NUnit.Framework;
using Plugins.EcsRxExtensions;
using Tests.Editor.SetUp;
using Zenject;

namespace Tests.Editor.Game.Systems
{
    public class SelfDestructionSystemTests : EcsRxApplicationTestBase
    {
        [Inject] private IEntityDatabase _entityDatabase;

        protected override void Install(DiContainer container)
        {
            //SelfDestructionSystem
        }

        protected override void BindSystems()
        {
            this.BindSystem<SelfDestructionSystem>();
        }

        [Test]
        public void SelfDestructionSystem_LifetimeZero_DestroyEntity()
        {
            //создат сущность
            var entity = _entityDatabase.GetCollection().CreateEntity();
            Assert.NotNull(entity);
            //задать время жизни через компонент
            var selfDestructComponent = entity.AddComponent<SelfDestructComponent>();
            selfDestructComponent.Lifetime = 0f;
            //проверить уничтожение сущности
            //Assert.IsNull(entity);
        }
        
    }
}