using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain
{
    public class OrderLine : IDiscountable<LineDiscount>
    {
        //public IOrderable OrderedItem { get; }
        public int AmountOrdered { get; private set; }
        public decimal UnitGoodsValue { get; private set; }
        public decimal GoodsValue => AmountOrdered * UnitGoodsValue;
        public decimal DiscountedAmount => EffectiveDiscounts.Sum(d => d.DiscountedAmount);
        public decimal ShippingCost { get; private set; }
        public List<LineDiscount> EffectiveDiscounts { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }

        protected OrderLine()
        {
            
        }

        public OrderLine(string description, int amount, decimal unitCost, decimal unitShippingCost)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            Id = Guid.NewGuid();
            EffectiveDiscounts = new List<LineDiscount>();
            Description = description;
            AmountOrdered = amount;
            UnitGoodsValue = unitCost;
            ShippingCost = unitShippingCost * AmountOrdered;
        }

        public void Apply(LineDiscount discount)
        {
            EffectiveDiscounts.Add(discount);
        }
    }
}
