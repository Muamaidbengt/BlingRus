namespace BlingRus.Domain
{
    public interface IHasAggregateCost
    {
        decimal AggregatedCost { get; }
        decimal AggregatedShippingCost { get; }
    }
}
