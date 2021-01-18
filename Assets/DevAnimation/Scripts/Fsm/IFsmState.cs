namespace Fsm
{
    public interface IFsmState<T>
    {
        void Enter(T entity);
        void Execute(T entity);
        void Exit(T entity);
    }
}