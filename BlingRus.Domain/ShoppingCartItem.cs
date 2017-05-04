using System;

namespace BlingRus.Domain
{
    public class ShoppingCartItem
    {
        public IOrderable Item { get; }
        public int Amount { get; }

        public ShoppingCartItem(int amount, IOrderable item)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Amount = amount;
        }
    }
}
