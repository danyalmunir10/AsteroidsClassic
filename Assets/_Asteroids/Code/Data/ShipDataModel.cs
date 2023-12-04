using System;

namespace Asteroids.Data
{
    [Serializable]
    public class ShipDataModel
    {
        public float ShipShieldDuration;
        public float AutoFireDelay;
        public float ShipMaxMovementSpeed;
        public float ShipMovementAcceleration;
        public float ShipDeacceleration;
        public float ShipRotationSpeed;
        public float RocketSpeed;
        public float RocketLifetime;
    }
}
