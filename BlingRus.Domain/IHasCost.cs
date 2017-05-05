namespace BlingRus.Domain
{
    public interface IHasCost
    {
        decimal UnitCost { get; }
        decimal UnitShippingCost { get; }
    }
}
