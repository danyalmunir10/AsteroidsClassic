using Cysharp.Threading.Tasks;

namespace Asteroids.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene);
    }
}