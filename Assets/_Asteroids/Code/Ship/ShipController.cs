using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Asteroids.Movement;
using Asteroids.Controls;
using Asteroids.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Asteroids.Gameplay
{ 
    public class ShipController : MonoBehaviour
    {
        private GameplayManager _gameplayManager;
        private SignalBus _signalBus;
        private MovementSystem _movementSystem;
        private ShipControls _shipControls;
        private ShipDataModel _shipData;
        private Rocket.Factory _rocketFactory;

        [SerializeField] private Transform _moveableObject;
        [SerializeField] private Transform _rocketSpawnPosition;
        [SerializeField] private GameObject _ship;
        [SerializeField] private GameObject _shipBroken;
        [SerializeField] private GameObject _flames;

        private bool _isPlayerAlive;
        private float _shieldDuration;
        private float _timeToFireAgain;
        private ShipMovement _movement;
        private List<TransformData> _transformsHistory;

        [Inject]
        private void Construct(GameplayManager gameplayManager, SignalBus signalBus, MovementSystem movementSystem, ShipControls shipControls, ShipDataModel shipData, Rocket.Factory rocketFactory)
        {
            _gameplayManager = gameplayManager;
            _signalBus = signalBus;
            _movementSystem = movementSystem;
            _shipControls = shipControls;
            _shipData = shipData;
            _rocketFactory = rocketFactory;
        }

        private void Awake()
        {
            _transformsHistory = new List<TransformData>();
            foreach (Transform t in _shipBroken.transform)
            {
                _transformsHistory.Add(new TransformData() { Position = t.localPosition, Rotation = t.localRotation });
            }
        }

        private void Update()
        {
            if (_isPlayerAlive)
            {
                _movement.ApplyThrust(_shipControls.Thrust);
                _movement.ApplyRotation(_shipControls.Steering);
                _flames.SetActive(_shipControls.Thrust != 0);

                if (_shipControls.IsFireButtonPressed && _timeToFireAgain <= 0)
                {
                    _timeToFireAgain = _shipData.AutoFireDelay;
                    FireRocket();
                }
                if (_timeToFireAgain > 0)
                {
                    _timeToFireAgain -= Time.deltaTime;
                }
            }
            if (_shieldDuration > 0)
                _shieldDuration -= Time.deltaTime;
        }

        public void Initialize()
        {
            _ship.SetActive(true);
            _shipBroken.SetActive(false);
            _flames.SetActive(false);
            _moveableObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            InitialzeMovement();

            _isPlayerAlive = true;
            _shieldDuration = _shipData.ShipShieldDuration;
        }

        public void Respawn()
        {
            int i = 0;
            foreach (Transform t in _shipBroken.transform)
            {
                t.localPosition = _transformsHistory[i].Position;
                t.localRotation = _transformsHistory[i].Rotation;
                i++;
            }

            _ship.SetActive(true);
            _shipBroken.SetActive(false);
            _flames.SetActive(false);
            _moveableObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            _isPlayerAlive = true;
            _shieldDuration = _shipData.ShipShieldDuration;
        }

        private void InitialzeMovement()
        {
            _movement = _movementSystem.AddMovement(MovementType.Ship, _moveableObject) as ShipMovement;
        }

        public async UniTask<bool> Destroy()
        {
            if (!_isPlayerAlive)
                return false;

            _isPlayerAlive = false;
            _ship.SetActive(false);
            _shipBroken.SetActive(true);

            foreach (Transform t in _shipBroken.transform)
            {
                t.DOLocalMove(Random.insideUnitSphere, 1).SetEase(Ease.OutQuad);
            }

            await UniTask.Delay(1200);

            _shipBroken.SetActive(false);
            _gameplayManager.OnPlayerShipDestroyed().Forget();
            return true;
        }

        private void FireRocket()
        {
            var rocket = _rocketFactory.Create(_shipData.RocketSpeed, _shipData.RocketLifetime);
            rocket.ShotFrom(_rocketSpawnPosition.position, _rocketSpawnPosition.up);
        }

        public void Cleanup()
        {
            _movementSystem.RemoveMovement(_movement);
            _movement = null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_shieldDuration > 0)
                return;

            if (collision.transform.CompareTag("Asteroid"))
            {
                Destroy().Forget();
            }
        }

        public class Factory : PlaceholderFactory<string, UniTask<ShipController>>
        {
            
        }

        public class TransformData
        {
            public Vector3 Position;
            public Quaternion Rotation;
        }
    }
}
