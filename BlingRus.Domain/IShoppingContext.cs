using System.Linq;

namespace BlingRus.Domain
{
    public interface IShoppingContext
    {
        IQueryable<ShoppingCart> Carts { get; }
        IQueryable<Jewelry> Catalog { get; }
        void Add(Order order);
        void Add(ShoppingCart cart);
        ShoppingCart CreateCart();
        void Save();
    }
}
