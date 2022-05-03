namespace Bombsite
{
    interface IBomb
    {
        bool Ready { get; }

        void Plant();

        void Explode();
    }
}
