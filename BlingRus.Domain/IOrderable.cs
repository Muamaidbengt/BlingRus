namespace BlingRus.Domain
{
    public interface IOrderable
    {
        decimal Price { get; }
        decimal ShippingCost { get; }
    }
}
