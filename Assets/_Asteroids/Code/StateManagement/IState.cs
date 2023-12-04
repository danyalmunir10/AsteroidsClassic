using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Asteroids.StateManagement
{
    public interface IState
    {
        UniTask Enter();
        UniTask Exit();
    }
}