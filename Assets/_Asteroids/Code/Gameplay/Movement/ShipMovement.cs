using Asteroids.Data;
using Asteroids.Utilities;
using UnityEngine;
using Zenject;

namespace Asteroids.Movement
{
    public class ShipMovement : MovementBase
    {
        private ShipDataModel _shipData;
        private float _rotationDelta;

        public ShipMovement(ShipDataModel shipData)
        {
            _shipData = shipData;
        }

        protected override void Movement(float deltaTime)
        {
            movementDirection += transform.up * movementSpeed * deltaTime;
            transform.position += movementDirection * deltaTime;
            transform.Rotate(-Vector3.forward, _rotationDelta * deltaTime);
            _rotationDelta = 0;
            if (true)// Decceleration enabled
            {
                float movementMagnitude = Mathf.Clamp(movementDirection.magnitude - (_shipData.ShipDeacceleration * deltaTime), 0, _shipData.ShipMaxMovementSpeed);
                movementDirection = movementDirection.normalized * movementMagnitude;
                if (movementSpeed > 0)
                {
                    movementSpeed -= _shipData.ShipDeacceleration;
                }
                else
                {
                    movementSpeed = 0;
                }
            }
        }

        public void ApplyThrust(float amount)
        {
            if (movementSpeed < _shipData.ShipMaxMovementSpeed)
            {
                movementSpeed += amount * _shipData.ShipMovementAcceleration;
            }
        }

        public void ApplyRotation(float delta)
        {
            _rotationDelta += delta * _shipData.ShipRotationSpeed;
        }

        public class Factory : PlaceholderFactory<ShipMovement>
        {
        }
    }
}
