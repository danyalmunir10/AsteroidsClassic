using UnityEngine;

namespace Asteroids.Utilities
{
    public class CameraHelper
    {
        private Camera _mainCamera;

        private int DEFAULT_WIDTH = 1920;
        private int DEFAULT_HEIGHT = 1080;
        private float DEFAULT_ORTHOGRAPHIC_SIZE = 5f;

        private float _currentAspectRatio;
        private float _currentCameraSize;

        public CameraHelper(Camera camera)
        {
            _mainCamera = camera;
            AdjustCameraSizeForResolution();
        }

        public void AdjustCameraSizeForResolution()
        {
            float defaultAspectRatio = (float)DEFAULT_WIDTH/DEFAULT_HEIGHT;
            _currentAspectRatio = _mainCamera.aspect;
            _currentCameraSize = (defaultAspectRatio / _currentAspectRatio) * DEFAULT_ORTHOGRAPHIC_SIZE;
            _mainCamera.orthographicSize = _currentCameraSize;
        }

        public float RightBoarder => GetHorizontalBoarderDistance();

        public float LeftBoarder => -GetHorizontalBoarderDistance();

        public float TopBoarder => GetVerticalBoarderDistance();

        public float BottomBoarder => -GetVerticalBoarderDistance();

        public float GetHorizontalBoarderDistance()
        {
            return _currentCameraSize * _currentAspectRatio;
        }

        public float GetVerticalBoarderDistance()
        {
            return _currentCameraSize;
        }
    }
}
