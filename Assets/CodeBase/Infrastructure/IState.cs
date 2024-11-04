namespace Assets.CodeBase.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}