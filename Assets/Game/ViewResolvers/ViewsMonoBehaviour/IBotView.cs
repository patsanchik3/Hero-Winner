namespace Game.ViewResolvers.ViewsMonoBehaviour
{
    public interface IBotView
    {
        int LastAnimationHash { get; }
        void PlayAnimation(int hash);
    }
}