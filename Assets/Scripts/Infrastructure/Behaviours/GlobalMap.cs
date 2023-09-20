using System.Collections.Generic;
using System.Linq;
using Infrastructure.Databases;
using Infrastructure.Models;
using Naninovel;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Behaviours
{
    public class GlobalMap : MonoBehaviour
    {
        [SerializeField] private MapNotificationVo[] _mapNotifications;
        [SerializeField] private MapSettingsDatabase _mapSettingsDatabase;

        private readonly ICustomVariableManager _variableManager = Engine.GetService<ICustomVariableManager>();

        private Dictionary<string, Image> _mapNotificationsDictionary;
        private string _currentMapName = string.Empty;

        private void Awake()
        {
            TurnNotificationsOff();
            _mapNotificationsDictionary = _mapNotifications.ToDictionary(mapNotification => mapNotification.Name,
                mapNotification => mapNotification.NotificationImage);
        }

        private void OnMapLeads()
        {
            _variableManager.TryGetVariableValue<string>(_mapSettingsDatabase.Settings.MapLeadsTrigger, out var map);

            if (string.IsNullOrEmpty(map))
                return;

            if (_currentMapName == map)
                return;

            if (_currentMapName != string.Empty)
                _mapNotificationsDictionary[_currentMapName].enabled = false;
            _mapNotificationsDictionary[map].enabled = true;
            _currentMapName = map;
        }

        private void OnMapLeadsActive()
        {
            _variableManager.TryGetVariableValue<bool>(_mapSettingsDatabase.Settings.MapLeadsActiveTrigger,
                out var isActive);
            if (!isActive)
            {
                _mapNotificationsDictionary[_currentMapName].enabled = false;
                _currentMapName = string.Empty;
            }
        }

        private void TurnNotificationsOff()
        {
            foreach (var mapNotification in _mapNotifications)
                mapNotification.NotificationImage.enabled = false;
        }
    }
}