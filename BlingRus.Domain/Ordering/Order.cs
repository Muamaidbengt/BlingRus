using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain.Shopping
{
    public class Order : IAdjustable<OrderPriceAdjustment>
    {
        public Guid Id { get; protected set; }

        private readonly List<OrderPriceAdjustment> _adjustments = new List<OrderPriceAdjustment>();
        public int TotalQuantityOrdered => OrderLines.Sum(l => l.QuantityOrdered);
        public decimal TotalGoodsValue => OrderLines.Sum(l => l.GoodsValue);
        public decimal TotalShippingCost => OrderLines.Sum(l => l.ShippingCost);
        public decimal TotalDiscountedAmount => OrderLines.Sum(l => l.DiscountedAmount) 
                                                + EffectiveAdjustments.Sum(d => d.DiscountedAmount);

        public decimal TotalAddedAmount => OrderLines.Sum(l => l.AddedAmount)
                                           + EffectiveAdjustments.Sum(d => d.AddedAmount);
        public string DeliveryName { get; set; }
        public string DeliveryAddress { get; set; }
        public string ConfirmationEmail { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime? CreditCardExpiration { get; set; }

        protected Order()
        {
            OrderLines = new List<OrderLine>();
        }

        public Order(IEnumerable<OrderLine> lines)
        {
            Id = Guid.NewGuid();
            OrderLines = new List<OrderLine>(lines);
        }

        public void Apply(OrderPriceAdjustment orderPriceAdjustment)
        {
            _adjustments.Add(orderPriceAdjustment);
        }

        public List<OrderLine> OrderLines { get; set; }
        public IEnumerable<OrderPriceAdjustment> EffectiveAdjustments => new List<OrderPriceAdjustment>(_adjustments);

        public decimal Sum => TotalGoodsValue + TotalShippingCost + TotalAddedAmount - TotalDiscountedAmount;
    }
}