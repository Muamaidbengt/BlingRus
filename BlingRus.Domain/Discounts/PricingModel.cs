using System.Collections.Generic;

namespace BlingRus.Domain.Discounts
{
    public class PricingModel
    {
        private readonly List<IOrderLinePriceAdjustmentCalculator> _orderLineAdjustmentCalculators = new List<IOrderLinePriceAdjustmentCalculator>();
        private readonly List<IOrderPriceAdjustmentCalculator> _orderAdjustmentCalculators = new List<IOrderPriceAdjustmentCalculator>();

        public IEnumerable<IOrderPriceAdjustmentCalculator> OrderAdjustmentCalculators => _orderAdjustmentCalculators;
        public IEnumerable<IOrderLinePriceAdjustmentCalculator> OrderLineAdjustmentCalculators => _orderLineAdjustmentCalculators;

        public LongTextCostCalculator LongTextCost { get; }

        public PricingModel()
        {
            LongTextCost = new LongTextCostCalculator(20, 30, 10, 50, 25);

            _orderAdjustmentCalculators.Add(new OrderAmountPriceAdjustmentCalculator(3, 10));
            _orderAdjustmentCalculators.Add(new FreeShippingDiscountCalculator(250));

            _orderLineAdjustmentCalculators.Add(new ValueAddedTaxCalculator(25));
            _orderLineAdjustmentCalculators.Add(LongTextCost);
            _orderLineAdjustmentCalculators.Add(new NthFreeDiscountCalculator(5));
        }
    }
}