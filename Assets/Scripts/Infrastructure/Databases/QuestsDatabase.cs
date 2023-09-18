using System;
using System.Collections.Generic;
using Infrastructure.Models;
using UnityEngine;

namespace Infrastructure.Databases
{
    [CreateAssetMenu(menuName = "Databases/QuestsDatabase", fileName = "QuestsDatabase")]
    public class QuestsDatabase : ScriptableObject
    {
        [SerializeField] private QuestVo[] _quests;
        private Dictionary<string, QuestVo> _questsDictionary;

        private void OnEnable()
        {
            _questsDictionary = new Dictionary<string, QuestVo>();

            foreach (var quest in _quests) 
                _questsDictionary.Add(quest.TriggerKey, quest);
        }

        public QuestVo GetQuest(string questKey)
        {
            try
            {
                return _questsDictionary[questKey];
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"[{nameof(QuestsDatabase)}] Quest" +
                    $" with key {questKey} was not present in the dictionary. {e.StackTrace}");
            }
        }
    }
}