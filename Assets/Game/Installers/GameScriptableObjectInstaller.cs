using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = "GameScriptableObjectInstaller", menuName = "Installers/GameScriptableObjectInstaller")]
    public class GameScriptableObjectInstaller : ScriptableObjectInstaller<GameScriptableObjectInstaller>
    {
        [SerializeField] private PrefabSettings _prefabSettings; 
        public override void InstallBindings()
        {
            Container.Bind<IPrefabSettings>().FromInstance(_prefabSettings);
        }
    }
}