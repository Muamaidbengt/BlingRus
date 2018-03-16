using System.Collections.Generic;

namespace BlingRus.Domain.Discounts
{
    public class DiscountModel
    {
        private readonly List<IOrderLineDiscountCalculator> _orderLineDiscountCalculators = new List<IOrderLineDiscountCalculator>();
        private readonly List<IOrderDiscountCalculator> _orderDiscountCalculators = new List<IOrderDiscountCalculator>();

        public IEnumerable<IOrderDiscountCalculator> OrderDiscountCalculators => _orderDiscountCalculators;
        public IEnumerable<IOrderLineDiscountCalculator> OrderLineDiscountCalculators => _orderLineDiscountCalculators;

        public DiscountModel()
        {
            _orderDiscountCalculators.Add(new OrderAmountDiscountCalculator(4, 10));
            _orderDiscountCalculators.Add(new FreeShippingDiscountCalculator(250));
            _orderLineDiscountCalculators.Add(new NthFreeDiscountCalculator(5));
        }
    }
}