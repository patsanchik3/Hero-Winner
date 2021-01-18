using EcsRx.Collections.Database;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Groups;
using EcsRx.Plugins.Views.Components;
using EcsRx.Unity.Dependencies;
using EcsRx.Unity.Systems;
using Game.Components;
using UnityEngine;

namespace Game.ViewResolvers
{
    public class BotViewResolver : DynamicViewResolverSystem
    {
        public BotViewResolver(IEventSystem eventSystem, IEntityDatabase entityDatabase, IUnityInstantiator instantiator) : base(eventSystem, entityDatabase, instantiator)
        {
        }

        public override IGroup Group { get; } = new Group(typeof(BotComponent), typeof(ViewComponent));
        public override GameObject CreateView(IEntity entity)
        {
            //var gameObject = Object.Instantiate(Vector3.zero, Quaternion.identity);
            var gameObject = new GameObject();
            gameObject.name = $"Bot-{entity.Id}";
            return gameObject;
        }

        public override void DestroyView(IEntity entity, GameObject view)
        {
            Object.Destroy(view);
        }
    }
}