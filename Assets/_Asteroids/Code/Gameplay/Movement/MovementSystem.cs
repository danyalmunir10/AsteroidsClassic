using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids.Movement
{ 
    public class MovementSystem : ITickable
    {
        private readonly List<MovementBase> _movementEntities;
        private readonly MovementFactory _movementFactory;

        public MovementSystem(MovementFactory movementFactory)
        { 
            _movementFactory = movementFactory;
            _movementEntities = new List<MovementBase>();
        }

        public void Tick()
        {
            foreach (var entity in _movementEntities)
            {
                entity.Update(Time.deltaTime);
            }
        }

        public MovementBase AddMovement(MovementType type, Transform transformToMove)
        {
            var movement = _movementFactory.CreateMovement(type);
            _movementEntities.Add(movement);
            movement.Initialize(transformToMove);
            return movement;
        }

        public void RemoveMovement(MovementBase movement) 
        {
            _movementEntities.Remove(movement);
        }
    }
}
