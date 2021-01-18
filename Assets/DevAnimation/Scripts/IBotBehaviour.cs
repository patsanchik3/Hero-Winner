using Fsm;

public interface IBotBehaviour
{
    void ChangeState(IFsmState<BotBehaviour> state);
}