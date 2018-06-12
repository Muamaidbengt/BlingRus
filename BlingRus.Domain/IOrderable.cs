namespace BlingRus.Domain
{
    public interface IOrderable : IHasCost
    {
        string Image { get; }
        string Name { get; }
    }
}
