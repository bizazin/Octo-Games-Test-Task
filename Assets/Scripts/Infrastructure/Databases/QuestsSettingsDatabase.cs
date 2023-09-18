using Infrastructure.Models;
using UnityEngine;

namespace Infrastructure.Databases
{
    [CreateAssetMenu(menuName = "Databases/QuestsSettingsDatabase", fileName = "QuestsSettingsDatabase")]
    public class QuestsSettingsDatabase : ScriptableObject
    {
        [SerializeField] private QuestsSettingsVo _questsSettings;

        public QuestsSettingsVo Settings => _questsSettings;

    }
}