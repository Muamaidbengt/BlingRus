using System.Collections.Generic;

namespace BlingRus.Domain
{
    public class ShoppingCart
    {
        private readonly List<ShoppingCartItem> _contents = new List<ShoppingCartItem>();

        public void Add(ShoppingCartItem item)
        {
            _contents.Add(item);
        }

        public void Remove(ShoppingCartItem item)
        {
            _contents.Remove(item);
        }

        public IEnumerable<ShoppingCartItem> Contents => new List<ShoppingCartItem>(_contents);
    }
}
