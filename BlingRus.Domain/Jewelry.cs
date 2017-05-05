namespace BlingRus.Domain
{
    public class Jewelry : IOrderable
    {
        public Jewelry(string description, JewelrySize size, string image)
        {
            Size = size;
            Image = image;
            Description = description;
        }

        public virtual decimal UnitCost => 100;
        public virtual decimal UnitShippingCost => 36;

        public JewelrySize Size { get; }
        public string Image { get; }
        public string Description { get; private set; }
    }
}
