using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Controls
{ 
    public class ShipControls : ITickable
    {
        private FixedJoystick _joystick;
        private Button _fireButton;

        public float Thrust { get; private set; }
        public float Steering { get; private set; }
        public bool IsFireButtonPressed { get; private set; }

        public ShipControls(FixedJoystick joystick, [Inject(Id = "FireButton")] Button fireButton)
        {
            _joystick = joystick;
            _fireButton = fireButton;

            var eventTrigger = _fireButton.AddComponent<EventTrigger>();
            EventTrigger.Entry pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener(OnFireButtonPressed);
            eventTrigger.triggers.Add(pointerDown);

            EventTrigger.Entry pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener(OnFireButtonReleased);
            eventTrigger.triggers.Add(pointerUp);
        }

        public void Tick()
        {
            Thrust = _joystick.Vertical > 0.3f ? _joystick.Vertical : 0;
            Steering = _joystick.Horizontal > 0.4f || _joystick.Horizontal < -0.4f ? _joystick.Horizontal : 0;
        }

        public void OnFireButtonPressed(BaseEventData data)
        {
            IsFireButtonPressed = true;
        }

        public void OnFireButtonReleased(BaseEventData data)
        {
            IsFireButtonPressed = false;
        }
    }
}
