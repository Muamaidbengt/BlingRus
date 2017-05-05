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

        public decimal UnitCost => Jewelry.UnitCost + Text.Length > 10 ? 50 : 0;
        public decimal UnitShippingCost => Jewelry.UnitShippingCost;

        public string Image => Jewelry.Image;

        public T Jewelry { get; }
        public string Description => $"{Jewelry.Description} with the text: \"{Text}\"";
    }
}
