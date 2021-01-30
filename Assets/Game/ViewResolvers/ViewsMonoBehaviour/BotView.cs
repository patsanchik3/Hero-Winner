using UnityEngine;

namespace Game.ViewResolvers.ViewsMonoBehaviour
{
    [RequireComponent(typeof(Animator))]
    public class BotView : MonoBehaviour
    {
        public Animator GetAnimator => _animator;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}