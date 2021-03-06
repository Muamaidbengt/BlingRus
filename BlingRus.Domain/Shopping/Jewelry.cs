﻿using System;

namespace BlingRus.Domain.Shopping
{
    public class Jewelry : IOrderable
    {
        protected Jewelry()
        {
        }

        public Jewelry(string name, Category category, int cost, string image, string description, string description2)
        {
            Name = name;
            Category = category;
            UnitCost = cost;
            Image = image;
            Description = description;
            Description2 = description2;
        }

        public Guid Id { get; private set; }

        public virtual decimal UnitCost { get; private set; }
        public virtual decimal UnitShippingCost => 36;

        public string Image { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Description2 { get; private set; }
        public Category Category { get; private set; }
    }
}
