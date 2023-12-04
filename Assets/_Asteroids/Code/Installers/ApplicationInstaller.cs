using Zenject;
using Asteroids.SceneManagement;
using Asteroids.StateManagement;
using Asteroids.Data;
using UnityEngine;

namespace Asteroids.Installers
{ 
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings GameSettings;

        public override void InstallBindings()
        {
            //BindGameSettings();

            BindSceneLoader();
            InstallGameStateMachine();
            BindAssetProvider();

            InstallSignals();
        }

        private void BindGameSettings()
        {
            Container.BindInstances(GameSettings.AssetsInfo);
        }

        private void BindSceneLoader()
        { 
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        }

        private void InstallGameStateMachine()
        { 
            GameStateMachineInstaller.Install(Container);
        }

        private void BindAssetProvider()
        { 
            Container.BindInterfacesTo<AssetProvider.AssetProvider>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}
