using Asteroids.Data;
using Asteroids.Utilities;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Asteroids.AssetProvider;
using Asteroids.Signals;
using System;
using Random = UnityEngine.Random;
using Asteroids.Gameplay.States;

namespace Asteroids.Gameplay
{ 
    public class AsteroidsManager : ITickable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly LevelData _levelData;
        private readonly AsteroidsDataModel _asteroidData;
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly CameraHelper _cameraHelper;
        private readonly IAssetProvider _assetProvider;
        private readonly AssetsInfo _assetsInfo;
        private readonly List<Sprite> _sprites;

        private bool _initialized = false;

        public AsteroidsManager(SignalBus signalBus, GameplayStateMachine gameplayStateMachine, LevelData levelData, AsteroidsDataModel asteroidData, Asteroid.Factory asteroidFactory, IAssetProvider assetProvider, AssetsInfo assetsInfo, CameraHelper cameraHelper)
        {
            _signalBus = signalBus;
            _gameplayStateMachine = gameplayStateMachine;
            _levelData = levelData;
            _asteroidData = asteroidData;
            _asteroidFactory = asteroidFactory;
            _cameraHelper = cameraHelper;
            _assetProvider = assetProvider;
            _assetsInfo = assetsInfo;
            _sprites = new List<Sprite>();

            _signalBus.Subscribe<AsteroidDestoryedSignal>(OnAsteroidDestroyed);
        }

        private int _spawnedAsteroidsCount;
        private List<Asteroid> _asteroids = new List<Asteroid>();
        private float _spawnDelay = 0;

        public async UniTask LoadLevel()
        {
            _sprites.Clear();

            var assetKeys = await _assetProvider.GetAssetsListByLabel<Texture2D>(_assetsInfo.AssetLabels.AsteroidTexture);

            foreach (var key in assetKeys)
            {
                var texture = await _assetProvider.Load<Texture2D>(key);
                _sprites.Add(Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
            }

            _spawnedAsteroidsCount = 0;

            Cleanup();
            for (int i = 0; i < _levelData.SpawnAsteroidsInitially; i++)
            {
                SpawnRandomAsteroid();
            }
            _initialized = true;
        }

        private void SpawnRandomAsteroid()
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = Random.Range(_cameraHelper.LeftBoarder, _cameraHelper.RightBoarder);
            spawnPos.y = Random.Range(_cameraHelper.BottomBoarder, _cameraHelper.TopBoarder);
            SpawnAsteroid(Random.Range(1, _levelData.MaxAsteroidLevel), spawnPos, null);
            _spawnedAsteroidsCount++;
        }

        private void Cleanup()
        {
            for (int i = _asteroids.Count - 1; i >= 0; i--)
            {
                _asteroids[i].Destroy();
            }
            _asteroids.Clear();
        }

        private void DestroyAsteroid(Asteroid asteroid)
        {
            BreakAsteroid(asteroid);
            if (_spawnedAsteroidsCount >= _levelData.MaxAsteroidsToSpawn && _asteroids.Count == 0)
            {
                _gameplayStateMachine.Enter<GameplayEndState>().Forget();
            }
        }

        private void BreakAsteroid(Asteroid asteroid)
        {
            if (asteroid.Level > 1)
            {
                SpawnAsteroid(asteroid.Level - 1, asteroid.transform.position, null);
                SpawnAsteroid(asteroid.Level - 1, asteroid.transform.position, null);
            }
            asteroid.Destroy();
            _asteroids.Remove(asteroid);
        }

        private void SpawnAsteroid(int level, Vector3 position, Vector3? direction = null)
        {
            var speed = Random.Range(_asteroidData.MinMovementSpeed, _asteroidData.MaxMovementSpeed);
            var scale = Random.Range(_asteroidData.MinScale, _asteroidData.MaxScale);
            var sprite = _sprites[Random.Range(0, _sprites.Count)];

            AsteroidSpawnData spawnData = new AsteroidSpawnData()
            {
                Speed = speed,
                Scale = scale,
                Level = level,
                Sprite = sprite
            };

            Asteroid asteroid = _asteroidFactory.Create(spawnData);
            asteroid.StartFrom(position, direction.HasValue ? direction.Value : Random.insideUnitCircle);
            _asteroids.Add(asteroid);
        }

        private void OnAsteroidDestroyed(AsteroidDestoryedSignal signal)
        {
            DestroyAsteroid(signal.Asteroid);
        }

        public void Tick()
        {
            if(!_initialized) return;

            if (_spawnedAsteroidsCount < _levelData.MaxAsteroidsToSpawn)
            {
                if (_spawnDelay > 0)
                {
                    _spawnDelay -= Time.deltaTime;
                }
                else
                {
                    SpawnRandomAsteroid();
                    _spawnDelay = Random.Range(_levelData.MinAsteroidSpawnDelay, _levelData.MaxAsteroidSpawnDelay);
                }
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<AsteroidDestoryedSignal>(OnAsteroidDestroyed);
        }
    }
}
