using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.AssetProvider
{
    public class PrefabFactoryAsync<TComponent> : IFactory<string, UniTask<TComponent>>
    {
        private IInstantiator _instantiator;
        private IAssetProvider _assetProvider;

        public PrefabFactoryAsync(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this._instantiator = instantiator;
            this._assetProvider = assetProvider;
        }

        public async UniTask<TComponent> Create(string assetKey)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(assetKey);
            GameObject newObject = _instantiator.InstantiatePrefab(prefab);
            return newObject.GetComponent<TComponent>();
        }
    }
}