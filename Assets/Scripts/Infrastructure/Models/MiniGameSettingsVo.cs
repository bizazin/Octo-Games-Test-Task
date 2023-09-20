using System;

namespace Infrastructure.Models
{
    [Serializable]
    public class MiniGameSettingsVo
    {
        public string SceneName;
        public string WinVariableName;
        public int MaxTurnsAmount;
    }
}