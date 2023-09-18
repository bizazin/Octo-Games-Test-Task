using System;

namespace Infrastructure.Models
{
    [Serializable]
    public class QuestsSettingsVo
    {
        public float AnimationDuration;
        public string CurrentQuestTrigger;
        public string CurrentQuestActiveTrigger;
    }
}