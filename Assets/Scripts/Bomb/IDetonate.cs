namespace Bombsite
{
    public interface IDetonate
    {
        float Delay { get; }

        void Invoke();
    }
}
