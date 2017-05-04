namespace BlingRus.Domain
{
    public abstract class Jewelry : IOrderable
    {
        protected Jewelry(JewelrySize size)
        {
            Size = size;
        }

        public virtual decimal Price => 100;
        public virtual decimal ShippingCost => 36;

        public JewelrySize Size { get; }
    }
}
