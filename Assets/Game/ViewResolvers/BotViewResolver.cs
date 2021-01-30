using EcsRx.Collections.Database;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.Views.Components;
using EcsRx.Unity.Dependencies;
using EcsRx.Unity.Systems;
using Game.Components;
using Game.Settings;
using Game.ViewResolvers.ViewsMonoBehaviour;
using UnityEngine;

namespace Game.ViewResolvers
{
    public class BotViewResolver : DynamicViewResolverSystem
    {
        private readonly IPrefabSettings _prefabSettings;

        public BotViewResolver(IEventSystem eventSystem, IEntityDatabase entityDatabase,
            IUnityInstantiator instantiator, IPrefabSettings prefabSettings) : base(eventSystem, entityDatabase,
            instantiator)
        {
            _prefabSettings = prefabSettings;
        }

        public override IGroup Group { get; } = new Group(typeof(BotComponent), typeof(ViewComponent));

        public override GameObject CreateView(IEntity entity)
        {
            Debug.Log("BotViewResolver");
            var prefab = _prefabSettings.GetBotPrefab;
            var gameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            gameObject.name = $"Bot-{entity.Id}";

            var botView = gameObject.GetComponent<BotView>();
            var botViewComponent = entity.AddComponent<BotViewComponent>();
            botViewComponent.BotView = botView;

            return gameObject;
        }

        public override void DestroyView(IEntity entity, GameObject view)
        {
            Object.Destroy(view);
        }
    }
}