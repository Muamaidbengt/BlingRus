namespace BlingRus.Domain.Shopping
{
    public interface IHasAggregateCost
    {
        decimal AggregatedCost { get; }
        decimal AggregatedShippingCost { get; }
    }
}
