using Infrastructure.Models;
using UnityEngine;

namespace Infrastructure.Databases
{
    [CreateAssetMenu(menuName = "Databases/MiniGameSettingsDatabase", fileName = "MiniGameSettingsDatabase")]
    public class MiniGameSettingsDatabase : ScriptableObject
    {
        [SerializeField] private MiniGameSettingsVo _miniGameSettings;

        public MiniGameSettingsVo Settings => _miniGameSettings;

    }
}