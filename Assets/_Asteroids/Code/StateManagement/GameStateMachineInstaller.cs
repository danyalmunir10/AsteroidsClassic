using UnityEngine;
using Zenject;

namespace Asteroids.StateManagement
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StatesFactory>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}