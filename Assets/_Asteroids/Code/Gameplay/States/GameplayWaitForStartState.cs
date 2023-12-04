using Asteroids.StateManagement;
using Asteroids.UI;
using Cysharp.Threading.Tasks;

namespace Asteroids.Gameplay.States
{
    public class GameplayWaitForStartState : IState
    {
        private readonly GameplayManager _gameplayManager;
        private readonly UIManager _uiManager;

        public GameplayWaitForStartState(GameplayManager gameplayManager, UIManager uIManager)
        {
            _gameplayManager = gameplayManager;
            _uiManager = uIManager;
        }

        public async UniTask Enter()
        {
            _uiManager.SetWaitForStart();
            _gameplayManager.SetWaitForStart();
        }

        public async UniTask Exit()
        {
            
        }
    }
}