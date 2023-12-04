using Asteroids.Movement;
using Asteroids.Utilities;
using Zenject;
using Asteroids.Signals;
using Asteroids.Controls;
using Asteroids.Gameplay;
using Asteroids.StateManagement;
using Asteroids.Gameplay.States;

namespace Asteroids.Installers
{ 
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameplayStateMachine();
            InstallMisc();
            InstallMovement();
            BindShipControls();
            DeclareGameplaySignals();

        }

        private void InstallGameplayStateMachine()
        {
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
            Container.Bind<GameplayStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplaySceneMachineInitializer>().AsSingle().NonLazy();
        }

        private void InstallMovement()
        {
            Container.Bind<MovementFactory>().AsSingle();

            Container.BindFactory<LinearMovement, LinearMovement.Factory>().WhenInjectedInto<MovementFactory>();
            Container.BindFactory<ShipMovement, ShipMovement.Factory>().WhenInjectedInto<MovementFactory>();

            Container.BindInterfacesAndSelfTo<MovementSystem>().AsSingle();
        }

        private void BindShipControls()
        {
            Container.BindInterfacesAndSelfTo<ShipControls>().AsSingle();
        }

        private void InstallMisc()
        {
            Container.Bind<CameraHelper>().AsSingle();
        }

        private void DeclareGameplaySignals()
        {
            Container.DeclareSignal<AsteroidDestoryedSignal>();
        }
    }
}
