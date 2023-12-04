using Asteroids.StateManagement;
using Asteroids.UI;
using Cysharp.Threading.Tasks;

namespace Asteroids.Gameplay.States
{
    public class GameplayLoopState : IState
    {
        private readonly GameplayManager _gameplayManager;
        private readonly UIManager _uiManager;

        public GameplayLoopState(GameplayManager gameplayManager, UIManager uiManager)
        {
            _gameplayManager = gameplayManager;
            _uiManager = uiManager;
        }

        public async UniTask Enter()
        {
            await _gameplayManager.Initialize();
            _uiManager.EnableGameplayHud();
        }

        public async UniTask Exit()
        {

        }
    }
}