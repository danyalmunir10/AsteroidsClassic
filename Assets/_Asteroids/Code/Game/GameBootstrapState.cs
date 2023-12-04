using Asteroids.StateManagement;
using Asteroids.AssetProvider;
using Cysharp.Threading.Tasks;

namespace Asteroids.Game
{ 
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IAssetProvider _assetProvider;

        public GameBootstrapState(GameStateMachine gameStateMachine, IAssetProvider assetProvider)
        {
            this._gameStateMachine = gameStateMachine;
            this._assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            await InitServices();

            _gameStateMachine.Enter<GameplayInitializationState>().Forget();
        }

        private async UniTask InitServices()
        {
            // init global services that may need initialization in some order here
            await _assetProvider.InitializeAsync();
        }

        public UniTask Exit() =>
            default;
    }
}
