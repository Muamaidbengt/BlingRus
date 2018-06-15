namespace BlingRus.Domain.Discounts
{
    public interface IOrderPriceAdjustmentCalculator
    {
        void ApplyTo(Order order);
    }
}
