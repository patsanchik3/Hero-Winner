using BotFsmStates;
using Fsm;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class BotBehaviour : MonoBehaviour, IBotBehaviour
{
    public Animator GetAnimator => _animator;
    private Animator _animator;
    private FiniteStateMachine<BotBehaviour> _fsm;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fsm = new FiniteStateMachine<BotBehaviour>();
        _fsm.Configure(this, new BotIdleSate());
    }

    public void ChangeState(IFsmState<BotBehaviour> state)
    {
        _fsm.ChangeState(state);
    }

    void Update()
    {
        _fsm.Update();
    }

    private void OnMouseDown()
    {
        ChangeState(new BotDamageSate());
    }
}