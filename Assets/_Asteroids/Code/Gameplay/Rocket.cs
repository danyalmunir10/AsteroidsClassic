using Asteroids.Movement;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class Rocket : MonoBehaviour, IPoolable<float, float, IMemoryPool>
    {
        [Inject] private MovementSystem _movementSystem;
        [Inject] private SignalBus _signalBus;
        float _startTime;
        float _speed;
        float _lifeTime;

        IMemoryPool _pool;
        private MovementBase _movement;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("Asteroid"))
            {
                _signalBus.Fire<AsteroidDestoryedSignal>(new AsteroidDestoryedSignal() { Asteroid = collision.transform.GetComponent<Asteroid>() });
                _pool.Despawn(this);
            }
        }

        public void Update()
        {
            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                _pool.Despawn(this);
            }
        }

        public void ShotFrom(Vector3 pos, Vector3 direction)
        {
            transform.position = pos;
            transform.up = direction;
            (_movement as LinearMovement).SetDirectionAndSpeed(transform.up, _speed);
        }

        public void OnSpawned(float speed, float lifeTime, IMemoryPool pool)
        {
            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;

            _startTime = Time.realtimeSinceStartup;

            _movement = _movementSystem.AddMovement(MovementType.Linear, transform);
        }

        public void OnDespawned()
        {
            _movementSystem.RemoveMovement(_movement);
            _movement = null;
            _pool = null;
        }

        public class Factory : PlaceholderFactory<float, float, Rocket>
        {
        }

        public class RocketPool : MonoPoolableMemoryPool<float, float, IMemoryPool, Rocket>
        {
        }
    }
}