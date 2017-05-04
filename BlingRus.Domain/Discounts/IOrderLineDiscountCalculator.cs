namespace BlingRus.Domain.Discounts
{
    public interface IOrderLineDiscountCalculator
    {
        void ApplyTo(OrderLine line);
    }
}
