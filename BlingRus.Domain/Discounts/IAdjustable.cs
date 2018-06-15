namespace BlingRus.Domain.Discounts
{
    public interface IAdjustable<in TAdjustment>
    {
        void Apply(TAdjustment adjustment);
    }
}
