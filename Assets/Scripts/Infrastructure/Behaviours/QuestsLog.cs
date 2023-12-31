using DG.Tweening;
using Infrastructure.Databases;
using Infrastructure.Models;
using Naninovel;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Behaviours
{
    public class QuestsLog : MonoBehaviour
    {
        [SerializeField] private Transform _questContainer;
        [SerializeField] private Text _questNameText;
        [SerializeField] private Text _questDescriptionText;
        [SerializeField] private Slider _questSlider;
        [SerializeField] private QuestsDatabase _questsDatabase;
        [SerializeField] private QuestsSettingsDatabase _questsSettingsDatabase;
        [SerializeField] private PlayScript _currentQuestTrigger;

        private readonly ICustomVariableManager _variableManager = Engine.GetService<ICustomVariableManager>();
        private QuestVo _currentQuest;
        private float _animationDuration;

        private void Awake()
        {
            _questContainer.localScale = Vector3.zero;
            _animationDuration = _questsSettingsDatabase.Settings.AnimationDuration;
        }

        private void OnCurrentQuest()
        {
            _variableManager.TryGetVariableValue<string>(_questsSettingsDatabase.Settings.CurrentQuestTrigger,
                out var questKey);

            if (string.IsNullOrEmpty(questKey))
                return;

            if (_currentQuest != null && _currentQuest.TriggerKey == questKey)
                return;
            var quest = _questsDatabase.GetQuest(questKey);
            var sequence = DOTween.Sequence();
            if (_currentQuest != null)
                sequence.Append(DisableQuest());
            sequence.Append(SetupAndEnableQuest(quest.Name, quest.Description));
            _currentQuest = quest;
            _currentQuestTrigger.Play();
        }
        
        private void OnCurrentQuestActive()
        {
            _variableManager.TryGetVariableValue<bool>(_questsSettingsDatabase.Settings.CurrentQuestActiveTrigger,
                out var isActive);
            if (!isActive)
            {
                _currentQuest = null;
                DisableQuest();
            }
        }

        private Sequence DisableQuest() =>
            DOTween.Sequence()
                .Append(_questSlider.DOValue(_questSlider.maxValue, _animationDuration))
                .Append(_questContainer.DOScale(0, _animationDuration));

        private Tween SetupAndEnableQuest(string questName, string description) =>
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _questNameText.text = questName;
                    _questDescriptionText.text = description;
                    _questSlider.value = _questSlider.minValue;
                })
                .Append(_questContainer.DOScale(1, _animationDuration));
    }
}