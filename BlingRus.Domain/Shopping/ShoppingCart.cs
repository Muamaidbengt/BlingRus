﻿using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.EnterpriseCollections;

namespace BlingRus.Domain.Shopping
{
    public class ShoppingCart : IHasAggregateCost
    {
        public int Id { get; protected set; }
        public string CreditCardNumber { get; set; }
        public DateTime? CreditCardExpiration { get; set; }
        
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

        internal IList<ShoppingCartItem> ContentsInternal { get; set; }

        public decimal AggregatedCost => ContentsInternal.Sum(c => c.AggregatedCost);
        public decimal AggregatedShippingCost => ContentsInternal.Sum(c => c.AggregatedShippingCost);

        protected ShoppingCart()
        {
            ContentsInternal = new EnterpriseListWrapper<ShoppingCartItem>(new List<ShoppingCartItem>());
        }

        public ShoppingCart(int id) : this()
        {
            Id = id;
        }

        public void Add(int amount, JewelrySize size, IOrderable item)
        {
            Add(new ShoppingCartItem(amount, size, item));
        }

        public void Add(ShoppingCartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            ContentsInternal.Add(item);
        }

        public void Remove(ShoppingCartItem item)
        {
            ContentsInternal.Remove(item);
        }

        public IEnumerable<ShoppingCartItem> Contents => new List<ShoppingCartItem>(ContentsInternal);
    }
}
