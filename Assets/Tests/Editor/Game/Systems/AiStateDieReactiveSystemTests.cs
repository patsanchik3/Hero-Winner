using EcsRx.Collections.Database;
using EcsRx.Extensions;
using Game.Blueprints;
using Game.Components;
using Game.Enums;
using Game.Settings;
using Game.Systems;
using Game.ViewResolvers;
using NSubstitute;
using NUnit.Framework;
using Plugins.EcsRxExtensions;
using Tests.Editor.SetUp;
using Zenject;

namespace Tests.Editor.Game.Systems
{
    public class AiStateDieReactiveSystemTests : EcsRxApplicationTestBase
    {
        private const string AssetPath = "Assets/Game/ScriptableObjects/PrefabSettings.asset";
        private const float LifeTime = 5f;
        private EAiState StartAnimationState = EAiState.Idle;
        
        [Inject] private IEntityDatabase _entityDatabase;
        [Inject] private IBotBlueprint _botBlueprint;
        protected override void Install(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<BotBlueprint>().AsSingle().WithArguments(StartAnimationState);
            
            var gameSettings = Substitute.For<IGameSettings>();
            gameSettings.DestroyBotTimeSeconds.Returns(LifeTime);
            container.Bind<IGameSettings>().FromInstance(gameSettings);
            
            var prefabSettings = UnityEditor.AssetDatabase.LoadAssetAtPath<PrefabSettings>(AssetPath);
            container.Bind<IPrefabSettings>().FromInstance(prefabSettings);
        }

        protected override void BindSystems()
        {
            this.BindSystem<BotViewResolver>();
            this.BindSystem<AiStateDieReactiveSystem>();
        }

        [Test]
        public void AiStateDieReactiveSystem_StartAnimationState_StateSetDie()
        {
            var entityPool = _entityDatabase.GetCollection();
            var entity = entityPool.CreateEntity(_botBlueprint);
            var hasSelfDestructComponent = entity.HasComponent<SelfDestructComponent>();
            Assert.IsFalse(hasSelfDestructComponent);
            
            var botView = entity.GetComponent<BotViewComponent>().BotView;

            var aiStateComponent = entity.GetComponent<AiStateComponent>();
            Assert.AreEqual(StartAnimationState, aiStateComponent.State.Value);
            
            Assert.AreNotEqual(AnimationStates.Die, botView.LastAnimationHash);
            
            entity.GetComponent<AiStateComponent>().State.Value = EAiState.Die;

            Assert.AreEqual(AnimationStates.Die, botView.LastAnimationHash);
            
            var selfDestructComponent = entity.GetComponent<SelfDestructComponent>();
            Assert.NotNull(selfDestructComponent);
            
            var componentLifeTime = selfDestructComponent.Lifetime;
            Assert.AreEqual(LifeTime, componentLifeTime);
        }
    }
}