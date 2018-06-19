using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain.Shopping
{
    public class OrderLine : IAdjustable<PriceLineAdjustment>
    {
        public int QuantityOrdered { get; private set; }
        public decimal UnitGoodsValue { get; private set; }
        public decimal GoodsValue => QuantityOrdered * UnitGoodsValue;
        public decimal DiscountedAmount => EffectiveAdjustments.Sum(d => d.DiscountedAmount);
        public decimal AddedAmount => EffectiveAdjustments.Sum(d => d.AddedAmount);
        public decimal ShippingCost { get; private set; }
        public List<PriceLineAdjustment> EffectiveAdjustments { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }
        public string Customization { get; private set; }

        protected OrderLine()
        {
            
        }

        public OrderLine(string description, int quantity, decimal unitCost, decimal unitShippingCost, string customization)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative");

            Id = Guid.NewGuid();
            EffectiveAdjustments = new List<PriceLineAdjustment>();
            Description = description;
            QuantityOrdered = quantity;
            UnitGoodsValue = unitCost;
            ShippingCost = unitShippingCost * QuantityOrdered;
            Customization = customization;
        }

        public void Apply(PriceLineAdjustment adjustment)
        {
            EffectiveAdjustments.Add(adjustment);
        }
    }
}