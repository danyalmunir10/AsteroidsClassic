using UnityEngine;
using Zenject;

namespace Asteroids.Data
{ 
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Create/GameplaySettings")]
    public class GameplaySettings : ScriptableObjectInstaller<GameplaySettings>
    {
        public ShipDataModel ShipDataModel;
        public AsteroidsDataModel AsteroidsDataModel;
        public LevelData LevelData;

        public override void InstallBindings()
        {
            Container.BindInstances(ShipDataModel);
            Container.BindInstances(AsteroidsDataModel);
            Container.BindInstances(LevelData);
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public int Lives;
        public int MaxAsteroidLevel;
        public int MaxAsteroidsToSpawn;
        public int SpawnAsteroidsInitially;
        public int MinAsteroidSpawnDelay;
        public int MaxAsteroidSpawnDelay;
        public int AsteroidKillScore;
    }
}
