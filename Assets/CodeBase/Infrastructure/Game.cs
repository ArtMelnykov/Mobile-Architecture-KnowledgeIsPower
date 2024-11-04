using Assets.CodeBase.Logic;
using Assets.CodeBase.Services.Input;

namespace Assets.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}