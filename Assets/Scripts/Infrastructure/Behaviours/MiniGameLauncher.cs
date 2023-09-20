using Infrastructure.Databases;
using Infrastructure.Services;
using Naninovel;
using UnityEngine;

namespace Infrastructure.Behaviours
{
    public class MiniGameLauncher : MonoBehaviour
    {
        [SerializeField] private MiniGameSettingsDatabase _miniGameSettingsDatabase;

        private void LaunchMiniGame() => LaunchAsync().Forget();

        private async UniTask LaunchAsync()
        {
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            var miniGameService = Engine.GetService<MiniGameService>();
            var variableManager = Engine.GetService<ICustomVariableManager>();
            var winVariableName = _miniGameSettingsDatabase.Settings.WinVariableName;
            
            scriptPlayer.Stop();

            var results = await miniGameService.PlayMiniGameAsync(_miniGameSettingsDatabase.Settings.SceneName);

            variableManager.TryGetVariableValue<bool>(winVariableName, out var isMiniGameWin);
            isMiniGameWin = results.amountOfTurns <= _miniGameSettingsDatabase.Settings.MaxTurnsAmount;
            variableManager.TrySetVariableValue(winVariableName, isMiniGameWin);

            scriptPlayer.Play();
        }
    }
}