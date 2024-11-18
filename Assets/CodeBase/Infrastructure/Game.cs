using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.States;
using Assets.CodeBase.Logic;

namespace Assets.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, ServiceLocator.Container);
        }
    }
}