namespace BlingRus.Domain.Discounts
{
    public interface IDiscountable<in TDiscount>
    {
        void Apply(TDiscount orderDiscount);
    }
}
