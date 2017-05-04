using System;

namespace BlingRus.Domain
{
    public class CustomizedJewelry<T> : IOrderable where T: Jewelry
    {
        public CustomizedJewelry(string text, T jewelry)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Jewelry = jewelry;
        }

        public string Text { get; }

        public decimal Price => Jewelry.Price + Text.Length > 10 ? 50 : 0;
        public decimal ShippingCost => Jewelry.ShippingCost;

        public T Jewelry { get; }
    }
}
