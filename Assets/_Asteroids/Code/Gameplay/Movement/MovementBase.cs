using UnityEngine;
using Asteroids.Utilities;
using Zenject;

namespace Asteroids.Movement
{
    public abstract class MovementBase
    {
        [Inject] private CameraHelper _cameraHelper;
        protected Transform transform;
        protected float movementSpeed;
        protected Vector3 movementDirection;

        public void Initialize(Transform transform)
        {
            this.transform = transform;
        }

        protected abstract void Movement(float deltaTime);

        // Update is called once per frame
        public void Update(float deltaTime)
        {
            Movement(deltaTime);
            //Horizontal boundary check
            if (transform.position.x > _cameraHelper.RightBoarder)
            {
                transform.position = new Vector3(_cameraHelper.LeftBoarder, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < _cameraHelper.LeftBoarder)
            {
                transform.position = new Vector3(_cameraHelper.RightBoarder, transform.position.y, transform.position.z);
            }

            //Vertical boundary check
            if (transform.position.y > _cameraHelper.TopBoarder)
            {
                transform.position = new Vector3(transform.position.x, _cameraHelper.BottomBoarder, transform.position.z);
            }
            else if (transform.position.y < _cameraHelper.BottomBoarder)
            {
                transform.position = new Vector3(transform.position.x, _cameraHelper.TopBoarder, transform.position.z);
            }
        }
    }
}
