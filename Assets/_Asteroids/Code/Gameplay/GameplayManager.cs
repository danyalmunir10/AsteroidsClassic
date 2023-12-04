using Asteroids.AssetProvider;
using Asteroids.Data;
using Asteroids.Gameplay.States;
using Asteroids.Signals;
using Asteroids.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class GameplayManager
    {
        private readonly SignalBus _signalBus;
        private readonly ShipController.Factory _shipFactory;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private AssetsInfo _assetsInfo;
        private AsteroidsManager _asteroidsManager;
        private LevelData _levelData;
        private GameplayHud _gameplayHud;

        private ShipController _shipController;
        private int _lives;
        private int _score;

        public GameplayManager(SignalBus signalBus, GameplayStateMachine gameplayStateMachine, ShipController.Factory shipFactory, AssetsInfo assetsInfo, AsteroidsManager asteroidsManager, LevelData levelData, GameplayHud gameplayHud) 
        {
            _signalBus = signalBus;
            _gameplayStateMachine = gameplayStateMachine;
            _shipFactory = shipFactory;
            _assetsInfo = assetsInfo;
            _asteroidsManager = asteroidsManager;
            _levelData = levelData;
            _gameplayHud = gameplayHud;
        }

        public async UniTask Initialize()
        {
            _score = 0;
            await _asteroidsManager.LoadLevel();
            _shipController = await _shipFactory.Create(_assetsInfo.AssetAddresses.Ship);
            _shipController.Initialize();
            _lives = _levelData.Lives;

            _gameplayHud.SetLives(_lives);
            _gameplayHud.SetScore(_score);

            _signalBus.Subscribe<AsteroidDestoryedSignal>(OnAsteroidDestroyed);
        }

        private void OnAsteroidDestroyed(AsteroidDestoryedSignal signal) 
        {
            _score += signal.Asteroid.Level * _levelData.AsteroidKillScore;
            _gameplayHud.SetScore(_score);
        }

        public async UniTask<bool> OnPlayerShipDestroyed()
        {
            _lives--;
            _gameplayHud.SetLives(_lives);
            //EventManager.DoFireOnLivesUpdatedEvent(_lives);
            if (_lives == 0)
            {
                _gameplayStateMachine.Enter<GameplayEndState>().Forget();
                return true;
            }
            else
            {
                await UniTask.Delay(1500);
                _shipController.Respawn();
                return false;
            }
        }

        public void SetWaitForStart()
        { 
        }

        public void SetEndGame()
        {
            _signalBus.Unsubscribe<AsteroidDestoryedSignal>(OnAsteroidDestroyed);
            _shipController.Cleanup();
            GameObject.Destroy(_shipController.gameObject);
            _shipController = null;
        }
    }
}
