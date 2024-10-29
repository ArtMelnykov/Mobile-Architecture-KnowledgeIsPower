using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        void Awake()
        {
            _game = new Game();

            DontDestroyOnLoad(this);
        }
    }
}