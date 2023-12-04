namespace Asteroids.AssetProvider
{
    [System.Serializable]
    public class AssetsInfo
    {
        public AssetLabels AssetLabels;
        public AssetAddresses AssetAddresses;
    }

    [System.Serializable]
    public class AssetLabels
    {
        public string Gameplay = "Gameplay";
        public string AsteroidTexture = "AsteroidTexture";
    }

    [System.Serializable]
    public class AssetAddresses
    {
        public string GameplayScene = "GameplayScene";
        public string Ship = "Ship";
    }
}