namespace Asteroids.Movement
{ 
    public class MovementFactory
    {
        private readonly LinearMovement.Factory _linearMovementFactory;
        private readonly ShipMovement.Factory _shipMovementFactory;

        public MovementFactory(LinearMovement.Factory linearMovementFactory, ShipMovement.Factory shipMovementFactory)
        { 
            _linearMovementFactory = linearMovementFactory;
            _shipMovementFactory = shipMovementFactory;
        }

        public MovementBase CreateMovement(MovementType type)
        {
            switch (type)
            {
                case MovementType.Linear:
                    {
                        return _linearMovementFactory.Create();
                    }
                case MovementType.Ship:
                    {
                        return _shipMovementFactory.Create();
                    }
            }

            return null;
        }
    }

    public enum MovementType
    {
        Linear,
        Ship
    }
}
