using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlingRus.Domain.Shopping
{
    public interface IShoppingContext
    {
        //IQueryable<ShoppingCart> Carts { get; }
        //IQueryable<Jewelry> Catalog { get; }
        Task<ShoppingCart> GetCartById(int id);
        Task<IEnumerable<Jewelry>> GetCatalog();
        Task<Jewelry> GetJewelryById(Guid id);
        Task Add(Order order);
        Task Add(ShoppingCart cart);
        Task<ShoppingCart> CreateCart();
        Task Save();
    }
}
