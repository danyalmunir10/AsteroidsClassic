using Asteroids.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Asteroids.AssetProvider;

namespace Asteroids.Installers
{ 
    public class GameplayInstaller : MonoInstaller
    {
        [Inject] private readonly IAssetProvider _assetProvider;

        public async override void InstallBindings()
        {
            BindShipController();
            await BindAsteroidFactory();
            await BindRocketFactory();

            Container.BindInterfacesAndSelfTo<GameplayManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsManager>().AsSingle();
        }

        private void BindShipController()
        {
            Container
                .BindFactory<string, UniTask<ShipController>, ShipController.Factory>()
                .FromFactory<PrefabFactoryAsync<ShipController>>();
        }

        private async UniTask<bool> BindRocketFactory()
        {
            var prefab = await _assetProvider.Load<GameObject>("Rocket");

            Container.BindFactory<float, float, Rocket, Rocket.Factory>()
                .FromPoolableMemoryPool<float, float, Rocket, Rocket.RocketPool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransformGroup("Rockets"));

            return true;
        }

        private async UniTask<bool> BindAsteroidFactory()
        {
            var prefab = await _assetProvider.Load<GameObject>("Asteroid");

            Container.BindFactory<AsteroidSpawnData, Asteroid, Asteroid.Factory>()
                .FromPoolableMemoryPool<AsteroidSpawnData, Asteroid, Asteroid.AsteroidPool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransformGroup("Asteroids"));

            return true;
        }
    }
}
