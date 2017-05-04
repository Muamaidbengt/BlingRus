using System;
using System.Collections.Generic;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain
{
    public class OrderLine : IDiscountable
    {
        private readonly List<Discount> _discounts = new List<Discount>();
        public IOrderable OrderedItem { get; }
        public int AmountOrdered { get; }
        public decimal GoodsValue => OrderedItem.Price * AmountOrdered;
        public decimal ShippingCost => OrderedItem.ShippingCost * AmountOrdered;
        public IEnumerable<Discount> EffectiveDiscounts => new List<Discount>(_discounts);

        public OrderLine(IOrderable orderable, int amount)
        {
            if(amount < 0)
                throw new ArgumentException("Amount cannot be negative");
            AmountOrdered = amount;
            OrderedItem = orderable;
        }

        public void Apply(Discount discount)
        {
            _discounts.Add(discount);
        }
    }
}
