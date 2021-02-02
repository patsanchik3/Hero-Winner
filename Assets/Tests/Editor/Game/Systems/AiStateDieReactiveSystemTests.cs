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
        
        [Inject] private IEntityDatabase _entityDatabase;
        [Inject] private IBotBlueprint _botBlueprint;
        protected override void Install(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<BotBlueprint>().AsSingle().WithArguments(EAiState.Die);
            
            var gameSettings = Substitute.For<IGameSettings>();
            gameSettings.DestroyBotTimeSeconds.Returns(0f);
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
        public void AiStateDieReactiveSystem_StartAnimationState_EqualBotBlueprint()
        {
            var entityPool = _entityDatabase.GetCollection();
            var entity = entityPool.CreateEntity(_botBlueprint);
            var botView = entity.GetComponent<BotViewComponent>().BotView;
            var lastAnimationHash = botView.LastAnimationHash;

            Assert.AreEqual(AnimationStates.Die, lastAnimationHash);
        }
    }
}