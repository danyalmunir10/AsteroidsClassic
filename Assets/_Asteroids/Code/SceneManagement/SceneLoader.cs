using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Asteroids.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string nextScene)
        {
            AsyncOperationHandle<SceneInstance> handler = Addressables.LoadSceneAsync(nextScene, LoadSceneMode.Single, false);

            await handler.ToUniTask();
            await handler.Result.ActivateAsync().ToUniTask();
        }
    }
}