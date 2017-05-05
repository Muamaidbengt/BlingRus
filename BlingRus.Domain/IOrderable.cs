namespace BlingRus.Domain
{
    public interface IOrderable : IHasCost
    {
        string Image { get; }
        string Description { get; }
    }
}
