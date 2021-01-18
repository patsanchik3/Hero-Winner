namespace Fsm
{
    public class FiniteStateMachine<T>
    {
        private T _owner;
        private IFsmState<T> _currentState;
        private IFsmState<T> _previousState;

        private readonly IFsmState<T> _globalState;

        public FiniteStateMachine()
        {
            _currentState = null;
            _previousState = null;
            _globalState = null;
        }

        public void Configure(T owner, IFsmState<T> initialState)
        {
            _owner = owner;
            ChangeState(initialState);
        }

        public void Update()
        {
            _globalState?.Execute(_owner);
            _currentState?.Execute(_owner);
        }

        public void ChangeState(IFsmState<T> newState)
        {
            _previousState = _currentState;

            _currentState?.Exit(_owner);

            _currentState = newState;

            _currentState?.Enter(_owner);
        }

        public void RevertToPreviousState()
        {
            if (_previousState != null)
                ChangeState(_previousState);
        }
    };
}