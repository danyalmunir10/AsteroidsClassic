using UnityEngine;
using Zenject;
using Asteroids.AssetProvider;

namespace Asteroids.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create/GameSettings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {
        public AssetsInfo AssetsInfo;

        public override void InstallBindings()
        {
            Container.BindInstances(AssetsInfo);
        }
    }
}