using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BlingRus.Domain.Shopping
{
    public static class ShoppingInitializer
    {
        public static void Initialize(ShoppingContext context, string inventoryJson)
        {
            context.Database.EnsureCreated();

            if (context.Catalog.Any())
            {
                return;
            }

            if(string.IsNullOrEmpty(inventoryJson))
                throw new ArgumentNullException(nameof(inventoryJson));

            var inventory = JObject.Parse(inventoryJson);
            foreach (var itemJson in inventory.First.First.Children())
            {
                var jewelry = itemJson.ToObject<Jewelry>();
                context.CatalogInternal.Add(jewelry);
            }

            context.SaveChanges();
        }
    }
}
