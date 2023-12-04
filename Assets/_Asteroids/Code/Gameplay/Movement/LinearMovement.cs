using UnityEngine;
using Asteroids.Utilities;
using Zenject;

namespace Asteroids.Movement
{
    public class LinearMovement : MovementBase
    {
        public void SetDirectionAndSpeed(Vector2 movementDirection, float movementSpeed)
        {
            base.movementDirection = movementDirection;
            base.movementSpeed = movementSpeed;
        }

        protected override void Movement(float deltaTime)
        {
            transform.position = transform.position + movementDirection * movementSpeed * deltaTime;
        }

        public class Factory : PlaceholderFactory<LinearMovement>
        {
        }
    }
}
