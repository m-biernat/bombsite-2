namespace Bombsite
{
    public interface IBomb
    {
        bool Active { get; }

        void Activate();

        bool Detonable { get; }

        bool Interactive { get; }

        int ID { get; }

        void SetID(int id);

        void Plant();

        void Trigger();
    }
}
