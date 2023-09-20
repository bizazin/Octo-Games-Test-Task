using Infrastructure.Models;
using UnityEngine;

namespace Infrastructure.Databases
{
    [CreateAssetMenu(menuName = "Databases/MapSettingsDatabase", fileName = "MapSettingsDatabase")]
    public class MapSettingsDatabase : ScriptableObject
    {
        [SerializeField] private MapSettingsVo _mapSettings;

        public MapSettingsVo Settings => _mapSettings;

    }
}