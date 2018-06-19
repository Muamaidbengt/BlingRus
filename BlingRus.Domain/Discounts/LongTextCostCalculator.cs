using System;
using System.Linq;
using BlingRus.Domain.Shopping;

namespace BlingRus.Domain.Discounts
{
    public class LongTextCostCalculator : IOrderLinePriceAdjustmentCalculator
    {
        private readonly int _maxFreeLength;
        private readonly int _maxTier1Length;
        private readonly int _subsequentTierLength;
        private readonly decimal _longTextCharge;
        private readonly decimal _linebreakCharge;

        private const char LineBreakIndicator = '/';

        public LongTextCostCalculator(int maxFreeLength, int maxTier1Length, int subsequentTierLength, decimal longTextCharge, decimal linebreakCharge)
        {
            _maxFreeLength = maxFreeLength;
            _maxTier1Length = maxTier1Length;
            _subsequentTierLength = subsequentTierLength;
            _longTextCharge = longTextCharge;
            _linebreakCharge = linebreakCharge;
        }

        public void ApplyTo(OrderLine line)
        {
            var tier = CalculateTextLengthTier(line.Customization);
            if(tier == 0)
                return;
            
            var nrOfLineBreaks = line.Customization.Count(letter => letter == LineBreakIndicator);
            var totalCharge = nrOfLineBreaks * _linebreakCharge + tier * _longTextCharge;

            line.Apply(new PriceLineAdjustment(
                line.QuantityOrdered, 
                "Premium engraving charge", 
                0, 
                totalCharge));
        }

        private int CalculateTextLengthTier(string customization)
        {
            var length = customization?.Length ?? 0;
            if (length <= _maxFreeLength)
                return 0;
            if (length <= _maxTier1Length)
                return 1;
            
            return 1 + Math.Max(0, (int)Math.Ceiling((double)(length - _maxTier1Length) / _subsequentTierLength));
        }
    }
}