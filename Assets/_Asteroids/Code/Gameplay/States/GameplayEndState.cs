using Asteroids.StateManagement;
using Asteroids.UI;
using Cysharp.Threading.Tasks;

namespace Asteroids.Gameplay.States
{
    public class GameplayEndState : IState
    {
        private readonly GameplayManager _gameplayManager;
        private readonly UIManager _uiManager;

        public GameplayEndState(GameplayManager gameplayManager, UIManager uiManager)
        {
            _gameplayManager = gameplayManager;
            _uiManager = uiManager;
        }

        public async UniTask Enter()
        {
            _uiManager.SetLevelEnd();
            _gameplayManager.SetEndGame();
        }

        public async UniTask Exit()
        {

        }
    }
}