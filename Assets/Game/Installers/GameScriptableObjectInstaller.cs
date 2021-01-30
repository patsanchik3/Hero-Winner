using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = "GameScriptableObjectInstaller", menuName = "Installers/GameScriptableObjectInstaller")]
    public class GameScriptableObjectInstaller : ScriptableObjectInstaller<GameScriptableObjectInstaller>
    {
        [SerializeField] private PrefabSettings _prefabSettings; 
        [SerializeField] private GameSettings _gameSettings; 
        public override void InstallBindings()
        {
            Container.Bind<IPrefabSettings>().FromInstance(_prefabSettings);
            Container.Bind<IGameSettings>().FromInstance(_gameSettings);
        }
    }
}