using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain
{
    public class Order : IDiscountable
    {
        private readonly List<Discount> _discounts = new List<Discount>();
        private readonly List<OrderLine> _lines;
        public int TotalAmountOrdered => _lines.Sum(l => l.AmountOrdered);
        public decimal TotalGoodsValue => _lines.Sum(l => l.GoodsValue);
        public decimal TotalShipping => _lines.Sum(l => l.ShippingCost);

        public Order(IEnumerable<OrderLine> lines)
        {
            _lines = new List<OrderLine>(lines);
        }

        public void Apply(Discount discount)
        {
            _discounts.Add(discount);
        }
    }
}
