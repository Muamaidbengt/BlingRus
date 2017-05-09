using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain
{
    public class Order : IDiscountable<OrderDiscount>
    {
        public Guid Id { get; protected set; }

        private readonly List<OrderDiscount> _discounts = new List<OrderDiscount>();
        public int TotalAmountOrdered => OrderLines.Sum(l => l.AmountOrdered);
        public decimal TotalGoodsValue => OrderLines.Sum(l => l.GoodsValue);
        public decimal TotalShippingCost => OrderLines.Sum(l => l.ShippingCost);
        public decimal TotalDiscountedAmount => OrderLines.Sum(l => l.DiscountedAmount) + EffectiveDiscounts.Sum(d => d.DiscountedAmount);

        protected Order()
        {
            OrderLines = new List<OrderLine>();
        }

        public Order(IEnumerable<OrderLine> lines)
        {
            Id = Guid.NewGuid();
            OrderLines = new List<OrderLine>(lines);
        }

        public void Apply(OrderDiscount orderDiscount)
        {
            _discounts.Add(orderDiscount);
        }

        public List<OrderLine> OrderLines { get; set; }
        public IEnumerable<OrderDiscount> EffectiveDiscounts => new List<OrderDiscount>(_discounts);

        public decimal Sum => TotalGoodsValue + TotalShippingCost - TotalDiscountedAmount;
    }
}
