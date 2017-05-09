using System;

namespace BlingRus.Domain
{
    public class ShoppingCartItem : IHasCost, IHasAggregateCost
    {
        public string Description { get; private set; }
        public int Amount { get; private set; }
        public Guid Id { get; private set; }
        public JewelrySize Size { get; private set; }

        public decimal UnitCost { get; private set; }
        public decimal UnitShippingCost { get; private set; }

        public decimal AggregatedCost => UnitCost * Amount;
        public decimal AggregatedShippingCost => UnitShippingCost * Amount;

        protected ShoppingCartItem()
        {
        }

        public ShoppingCartItem(int amount, JewelrySize size, IOrderable item)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            Id = Guid.NewGuid();
            Size = size;
            Description = item.Description;
            UnitCost = item.UnitCost;
            UnitShippingCost = item.UnitShippingCost;
            Amount = amount;
        }
    }
}
