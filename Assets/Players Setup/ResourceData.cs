
namespace RTS
{
    public class ResourceData
    {

        public int Gold { get; private set; }
        public int Timber { get; private set; }
        public int Food { get; private set; }
        public int MaxFood { get; private set; }

        public ResourceData(int g, int t)
        {
            Gold = g;
            Timber = t;
        }

        public void AmendGold(int amount)
        {
            Gold += amount;
        }

        public void AmendTimber(int amount)
        {
            Timber += amount;
        }

        public void AmendFood(int amount)
        {
            Food += amount;
        }

        public void AmendMaxFood(int amount)
        {
            MaxFood += amount;
        }
    }
}