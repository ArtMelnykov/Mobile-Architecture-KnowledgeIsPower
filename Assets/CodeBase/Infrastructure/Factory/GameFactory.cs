using Assets.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at) => 
            _assets.Instantiate(AssetPath.HeroPath, at.transform.position);

        public void CreateHud() => 
            _assets.Instantiate(AssetPath.HudPath);
    }
}