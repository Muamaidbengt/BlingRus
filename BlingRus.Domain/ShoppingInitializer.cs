﻿using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BlingRus.Domain
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

            var inventory = JObject.Parse(inventoryJson);
            foreach (var itemJson in inventory.First.First.Children())
            {
                var name = itemJson.Value<string>("name");
                var image = itemJson.Value<string>("image");
                var description = itemJson.Value<string>("description");
                var description2 = itemJson.Value<string>("description2");
                var categoryRaw = itemJson.Value<string>("category");
                var category = Enum.Parse<Category>(categoryRaw);

                var jewelry = new Jewelry(name, category, image, description, description2);
                context.CatalogInternal.Add(jewelry);
            }

            context.SaveChanges();
        }
    }
}
