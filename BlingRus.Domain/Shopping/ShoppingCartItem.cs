using System;

namespace BlingRus.Domain.Shopping
{
    public class ShoppingCartItem : IHasCost, IHasAggregateCost
    {
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public Guid Id { get; private set; }
        public JewelrySize Size { get; private set; }
        public string Customization { get; private set; }

        public decimal UnitCost { get; private set; }
        public decimal UnitShippingCost { get; private set; }

        public decimal AggregatedCost => UnitCost * Quantity;
        public decimal AggregatedShippingCost => UnitShippingCost * Quantity;

        protected ShoppingCartItem()
        {
        }

        public ShoppingCartItem(int quantity, JewelrySize size, IOrderable item)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative");
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            Id = Guid.NewGuid();
            Size = size;
            Description = $"{item.Name} ({size})";
            UnitCost = item.UnitCost;
            UnitShippingCost = item.UnitShippingCost;
            Quantity = quantity;
            Customization = (item as ICustomizedJewelry)?.Text;
        }
    }
}