using System;

namespace BlingRus.Domain
{
    public class CustomizedJewelry<T> : ICustomizedJewelry, IOrderable where T: Jewelry
    {
        public CustomizedJewelry(string text, T jewelry)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Jewelry = jewelry;
        }

        public string Text { get; }

        public decimal UnitCost => Jewelry.UnitCost;
        public decimal UnitShippingCost => Jewelry.UnitShippingCost;

        public string Image => Jewelry.Image;

        public T Jewelry { get; }
        public string Name => Jewelry.Name;
    }

    public interface ICustomizedJewelry
    {
        string Text { get; }
    }
}
