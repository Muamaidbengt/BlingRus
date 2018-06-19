namespace BlingRus.Domain.Shopping
{
    public interface IOrderable : IHasCost
    {
        string Image { get; }
        string Name { get; }
    }
}
