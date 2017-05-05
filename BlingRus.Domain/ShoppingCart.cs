using System;
using System.Collections.Generic;
using System.Linq;

namespace BlingRus.Domain
{
    public class ShoppingCart : IHasAggregateCost
    {
        public Guid Id { get; protected set; }

        private readonly List<ShoppingCartItem> _contents = new List<ShoppingCartItem>();

        public decimal AggregatedCost => _contents.Sum(c => c.AggregatedCost);
        public decimal AggregatedShippingCost => _contents.Sum(c => c.AggregatedShippingCost);

        public ShoppingCart()
        {
            Id = Guid.NewGuid();
        }

        public void Add(int amount, IOrderable item)
        {
            Add(new ShoppingCartItem(amount, item));
        }

        public void Add(ShoppingCartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _contents.Add(item);
        }

        public void Remove(ShoppingCartItem item)
        {
            _contents.Remove(item);
        }

        public IEnumerable<ShoppingCartItem> Contents => new List<ShoppingCartItem>(_contents);
    }
}
