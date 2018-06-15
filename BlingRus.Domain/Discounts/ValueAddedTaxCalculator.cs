namespace BlingRus.Domain.Discounts
{
    public class ValueAddedTaxCalculator : IOrderLinePriceAdjustmentCalculator
    {
        private readonly decimal _taxRate;

        public ValueAddedTaxCalculator(decimal taxRate)
        {
            _taxRate = taxRate;
        }

        public void ApplyTo(OrderLine line)
        {
            line.Apply(new PriceLineAdjustment($"Value added tax ({_taxRate:0.0}%)", 0, line.GoodsValue * (_taxRate / 100)));
        }
    }
}