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
        public IList<ShoppingCartItem> SecuredContents => new EnterpriseListWrapper<ShoppingCartItem>(ContentsInternal);
        public decimal AggregatedCost => ContentsInternal.Sum(c => c.AggregatedCost);
        public decimal AggregatedShippingCost => ContentsInternal.Sum(c => c.AggregatedShippingCost);
        public int AggregatedQuantity => ContentsInternal.Sum(c => c.Quantity);

        protected ShoppingCart()
        {
            ContentsInternal = new List<ShoppingCartItem>();
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
            SecuredContents.Add(item);
        }

        public void Remove(ShoppingCartItem item)
        {
            SecuredContents.Remove(item);
        }

        public int Count => SecuredContents.Count;
    }
}
