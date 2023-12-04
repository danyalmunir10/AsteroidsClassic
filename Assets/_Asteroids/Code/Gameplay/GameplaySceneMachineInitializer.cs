using Asteroids.Gameplay.States;
using Asteroids.StateManagement;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Asteroids.Gameplay 
{
    public class GameplaySceneMachineInitializer : IInitializable
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly StatesFactory statesFactory;

        public GameplaySceneMachineInitializer(GameplayStateMachine gameplayStateMachine, StatesFactory statesFactory)
        {
            this._gameplayStateMachine = gameplayStateMachine;
            this.statesFactory = statesFactory;
        }

        public void Initialize()
        {
            _gameplayStateMachine.RegisterState(statesFactory.Create<GameplayWaitForStartState>());
            _gameplayStateMachine.RegisterState(statesFactory.Create<GameplayLoopState>());
            _gameplayStateMachine.RegisterState(statesFactory.Create<GameplayEndState>());

            // go to the first scene state
            _gameplayStateMachine.Enter<GameplayWaitForStartState>().Forget();
        }
    }
}
