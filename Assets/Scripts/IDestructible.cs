namespace Bombsite
{
    public interface IDestructible
    {
        bool Destroyed { get; }

        void Hit();
    }
}
