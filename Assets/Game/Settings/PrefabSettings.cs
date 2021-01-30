using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/PrefabSettings", fileName = "PrefabSettings")]
    public class PrefabSettings : ScriptableObject, IPrefabSettings
    {
        [SerializeField] private GameObject _bot;
        public GameObject GetBotPrefab => _bot;
    }
}