namespace BlingRus.Domain.Discounts
{
    public interface IDiscountable
    {
        void Apply(Discount discount);
    }
}
