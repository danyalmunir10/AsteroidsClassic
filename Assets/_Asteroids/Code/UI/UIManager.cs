using Asteroids.Gameplay.States;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.UI
{ 
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playScreen;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameplayHud _gameplayHud;

        private GameplayStateMachine _stateMachine;

        [Inject]
        private void Construct(GameplayStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void SetWaitForStart()
        {
            _playScreen.SetActive(true);
            _gameplayHud.gameObject.SetActive(false);
            _endScreen.SetActive(false); 
        }

        public void SetLevelEnd()
        {
            _playScreen.SetActive(false);
            _gameplayHud.gameObject.SetActive(false);
            _endScreen.SetActive(true);
        }

        public void OnPlayButtonPressed()
        {
            _stateMachine.Enter<GameplayLoopState>().Forget();
        }

        public void OnPlayAgainButtonPressed()
        {
            _stateMachine.Enter<GameplayWaitForStartState>().Forget();
        }

        public void EnableGameplayHud()
        {
            _playScreen.SetActive(false);
            _gameplayHud.gameObject.SetActive(true);
            _endScreen.SetActive(false);
        }
    }
}
