using Asteroids.Movement;
using UnityEngine;
using Zenject;

namespace Asteroids.Gameplay
{
    public class Asteroid : MonoBehaviour, IPoolable<AsteroidSpawnData, IMemoryPool>
    {
        [Inject] private MovementSystem _movementSystem;
        [SerializeField] private SpriteRenderer _renderer;

        public int Level => _spawnData.Level;
        private AsteroidSpawnData _spawnData;
        private IMemoryPool _pool;
        private MovementBase _movement;

        public void StartFrom(Vector3 pos, Vector3 direction)
        {
            transform.position = pos;
            transform.up = direction;
            (_movement as LinearMovement).SetDirectionAndSpeed(transform.up, _spawnData.Speed);
        }

        public void OnSpawned(AsteroidSpawnData spawnData, IMemoryPool pool)
        {
            _spawnData = spawnData;
            _pool = pool;
            _renderer.sprite = _spawnData.Sprite;

            _movement = _movementSystem.AddMovement(MovementType.Linear, transform);
            transform.localScale = Vector3.one * _spawnData.Scale * _spawnData.Level;
        }

        public void OnDespawned()
        {
            _movementSystem.RemoveMovement(_movement);
            _movement = null;
            _pool = null;
        }

        public void Destroy()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<AsteroidSpawnData, Asteroid>
        {
        }

        public class AsteroidPool : MonoPoolableMemoryPool<AsteroidSpawnData, IMemoryPool, Asteroid>
        {
        }
    }

    public class AsteroidSpawnData
    {
        public float Speed;
        public float Scale;
        public int Level;
        public Sprite Sprite;
    }
}
