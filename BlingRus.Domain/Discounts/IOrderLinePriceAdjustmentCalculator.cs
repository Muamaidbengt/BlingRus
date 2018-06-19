using BlingRus.Domain.Shopping;

namespace BlingRus.Domain.Discounts
{
    public interface IOrderLinePriceAdjustmentCalculator
    {
        void ApplyTo(OrderLine line);
    }
}
