namespace BlingRus.Domain.Shopping
{
    public interface IHasCost
    {
        decimal UnitCost { get; }
        decimal UnitShippingCost { get; }
    }
}
