using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private float _destroyBotTimeSeconds;
        public float DestroyBotTimeSeconds => _destroyBotTimeSeconds;
    }
}