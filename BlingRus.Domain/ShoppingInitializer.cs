using System.Linq;

namespace BlingRus.Domain
{
    public static class ShoppingInitializer
    {
        public static void Initialize(ShoppingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Carts.Any())
            {
                return;
            }

            context.SaveChanges();
        }
    }
}
