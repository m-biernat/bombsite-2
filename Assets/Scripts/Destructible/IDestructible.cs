namespace Bombsite
{
    public interface IDestructible
    {
        bool Destructed { get; }

        void Hit();
    }
}
