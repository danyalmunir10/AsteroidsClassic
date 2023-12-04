using Asteroids.StateManagement;

namespace Asteroids.Gameplay.States
{ 
    public class GameplayStateMachine : StateMachine
    {
    }

    public enum GameplayStates
    {
        GameplayWaitForStart,
        GameplayLoop,
        GameplayEnd
    }
}
