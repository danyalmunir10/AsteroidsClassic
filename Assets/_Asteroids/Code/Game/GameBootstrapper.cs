using UnityEngine;
using Asteroids.StateManagement;
using Zenject;

namespace Asteroids.Game
{ 
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        void Construct(GameStateMachine gameStateMachine, StatesFactory statesFactory)
        {
            this._gameStateMachine = gameStateMachine;
            this._statesFactory = statesFactory;
        }

        private void Start()
        {
            _gameStateMachine.RegisterState(_statesFactory.Create<GameBootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameplayInitializationState>());

            _gameStateMachine.Enter<GameBootstrapState>();

            DontDestroyOnLoad(this);
        }

        public class Factory : PlaceholderFactory<GameBootstrapper>
        {
        }
    }
}
