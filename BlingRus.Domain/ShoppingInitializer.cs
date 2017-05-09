using System.Linq;

namespace BlingRus.Domain
{
    public static class ShoppingInitializer
    {
        public static void Initialize(ShoppingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Catalog.Any())
            {
                return;
            }

            context.CatalogInternal.Add(new Jewelry("The Bling Ring", "BlingRUs_Ring.jpg"));
            context.CatalogInternal.Add(new Jewelry("Teh Bling Thing", "BlingRUs_Dogtags.jpg"));
            context.CatalogInternal.Add(new Jewelry("The Bling Fling by Gunde™", "BlingRUs_Armband.jpg"));

            context.SaveChanges();
        }
    }
}
