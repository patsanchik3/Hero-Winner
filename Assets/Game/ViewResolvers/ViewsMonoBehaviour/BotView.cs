using UnityEngine;

namespace Game.ViewResolvers.ViewsMonoBehaviour
{
    [RequireComponent(typeof(Animator))]
    public class BotView : MonoBehaviour, IBotView
    {
        public int LastAnimationHash { get; private set; }
        public Animator GetAnimator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();
                
                return _animator;
            }
        }

        private Animator _animator;
        
        public void PlayAnimation(int hash)
        {
            LastAnimationHash = hash;
            GetAnimator.SetTrigger(hash);
        }
    }
}