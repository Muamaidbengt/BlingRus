using System;

namespace BlingRus.Domain
{
    public class Jewelry : IOrderable
    {
        protected Jewelry()
        {
        }

        public Jewelry(string description, string image)
        {
            Image = image;
            Description = description;
        }

        public Guid Id { get; private set; }

        public virtual decimal UnitCost => 100;
        public virtual decimal UnitShippingCost => 36;

        public string Image { get; private set; }
        public string Description { get; private set; }
    }
}
