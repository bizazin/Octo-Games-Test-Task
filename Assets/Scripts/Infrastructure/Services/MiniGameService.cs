using System.Threading.Tasks;
using Naninovel;
using UnityEngine.SceneManagement;
using DTT.MinigameMemory;
using Object = UnityEngine.Object;

namespace Infrastructure.Services
{
    [InitializeAtRuntime]
    public class MiniGameService : IEngineService
    {
        private MemoryGameManager _gameManager;
        private MemoryGameResults _gameResults;
        private TaskCompletionSource<MemoryGameResults> _completionSource;
        private const string MiniGameSceneName = "Demo";

        public UniTask InitializeServiceAsync() => UniTask.CompletedTask;

        public void ResetService()
        {
            if (_gameManager != null)
            {
                _gameManager.Finish -= StoreResults;
                _gameManager = null;
            }
            _gameResults = null;
        }

        public void DestroyService() => ResetService();

        public async UniTask<MemoryGameResults> PlayMiniGameAsync()
        {
            await SceneManager.LoadSceneAsync(MiniGameSceneName, LoadSceneMode.Additive);
            
            _gameManager = Object.FindObjectOfType<MemoryGameManager>();
            _gameManager.Finish += StoreResults;

            _completionSource = new TaskCompletionSource<MemoryGameResults>();
            await _completionSource.Task;

            await SceneManager.UnloadSceneAsync(MiniGameSceneName);
            
            return _gameResults;
        }

        private void StoreResults(MemoryGameResults results)
        {
            if (_gameResults == null)
            {
                _gameResults = results;
                _completionSource.SetResult(_gameResults);
                return;
            }

            var totalTimeTaken = _gameResults.timeTaken + results.timeTaken;
            var totalTurnsTaken = _gameResults.amountOfTurns + results.amountOfTurns;
            _gameResults = new MemoryGameResults(totalTimeTaken, totalTurnsTaken);
            _completionSource.SetResult(_gameResults);
        }
    }
}