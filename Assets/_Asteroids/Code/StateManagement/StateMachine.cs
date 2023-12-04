using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Asteroids.StateManagement
{
    public abstract class StateMachine : IStateMachine
    {
        private readonly Dictionary<System.Type, IState> _registeredStates;
        private IState _currentState;

        public StateMachine() => 
            _registeredStates = new Dictionary<Type, IState>();

        public async UniTask Enter<TState>() where TState : class, IState
        {
            TState newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public void RegisterState<TState>(TState state) where TState : IState =>
            _registeredStates.Add(typeof(TState), state);

        private async UniTask<TState> ChangeState<TState>() where TState : class, IState
        {
            if(_currentState != null)
                await _currentState.Exit();
      
            TState state = GetState<TState>();
            _currentState = state;
      
            return state;
        }
    
        private TState GetState<TState>() where TState : class, IState => 
            _registeredStates[typeof(TState)] as TState;
    }
}