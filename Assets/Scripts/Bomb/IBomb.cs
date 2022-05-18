namespace Bombsite
{
    public interface IBomb
    {
        bool Active { get; }

        bool Registerable { get; }

        bool IsTrigger { get; }

        int ID { get; }

        void Activate();

        void SetID(int id);

        void Plant();

        void Trigger();
    }
}
