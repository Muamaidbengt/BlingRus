namespace BlingRus.Domain.Discounts
{
    public interface IOrderDiscountCalculator
    {
        void ApplyTo(Order order);
    }
}
