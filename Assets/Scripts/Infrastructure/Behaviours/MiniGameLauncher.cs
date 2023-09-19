using Infrastructure.Services;
using Naninovel;
using UnityEngine;

namespace Infrastructure.Behaviours
{
    public class MiniGameLauncher : MonoBehaviour
    {
        private IScriptPlayer _scriptPlayer;
        private MiniGameService _miniGameService;

        private void Start()
        {
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            _miniGameService = Engine.GetService<MiniGameService>();
        }

        public void LaunchMiniGame() => LaunchAsync().Forget();

        private async UniTask LaunchAsync()
        {
            _scriptPlayer.Stop();

            var results = await _miniGameService.PlayMiniGameAsync();


            _scriptPlayer.Play();
        }
    }
}