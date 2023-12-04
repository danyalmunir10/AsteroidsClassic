using Asteroids.StateManagement;
using Asteroids.SceneManagement;
using Asteroids.AssetProvider;
using Cysharp.Threading.Tasks;

namespace Asteroids.Game
{ 
    public class GameplayInitializationState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly AssetsInfo _assetsInfo;

        public GameplayInitializationState(ISceneLoader sceneLoader, IAssetProvider assetProvider, AssetsInfo assetsInfo)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _assetsInfo = assetsInfo;
        }

        public async UniTask Enter()
        {
            await _assetProvider.WarmupAssetsByLabel(_assetsInfo.AssetLabels.Gameplay);
            await _sceneLoader.Load(_assetsInfo.AssetAddresses.GameplayScene);
        }

        public async UniTask Exit()
        {
            await _assetProvider.ReleaseAssetsByLabel(_assetsInfo.AssetLabels.Gameplay);
        }
    }
}
