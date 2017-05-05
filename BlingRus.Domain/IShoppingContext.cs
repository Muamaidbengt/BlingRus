using System.Linq;

namespace BlingRus.Domain
{
    public interface IShoppingContext
    {
        IQueryable<ShoppingCart> Carts { get; }
        void Add(Order order);
        void Add(ShoppingCart cart);
        void Save();
    }
}
