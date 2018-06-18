namespace BlingRus.Domain.Discounts
{
    public class LongTextCostCalculator : IOrderLinePriceAdjustmentCalculator
    {
        private readonly int _textLength;
        private readonly decimal _charge;

        public LongTextCostCalculator(int textLength, decimal charge)
        {
            _textLength = textLength;
            _charge = charge;
        }

        public void ApplyTo(OrderLine line)
        {
            if (string.IsNullOrEmpty(line.Customization) || line.Customization.Length <= _textLength)
                return;
            line.Apply(new PriceLineAdjustment($"Text longer than {_textLength} characters", 0, _charge * line.QuantityOrdered));
        }
    }
}