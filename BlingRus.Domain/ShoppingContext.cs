using System;
using System.Linq;
using BlingRus.Domain.Discounts;
using Microsoft.EntityFrameworkCore;

namespace BlingRus.Domain
{
    public class ShoppingContext : DbContext, IShoppingContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
        }

        public IQueryable<ShoppingCart> Carts => CartsInternal.Include(c => c.ContentsInternal);
        public IQueryable<Jewelry> Catalog => CatalogInternal;

        public void Add(Order order)
        {
            OrdersInternal.Add(order);
        }

        public void Add(ShoppingCart cart)
        {
            CartsInternal.Add(cart);
        }

        public ShoppingCart CreateCart()
        {
            var maxId = CartsInternal.Any() 
                ? CartsInternal.Max(c => c.Id) 
                : 821103;
            return new ShoppingCart(maxId + 1);
        }

        public void Save()
        {
            SaveChanges();
        }

        internal DbSet<Jewelry> CatalogInternal { get; set; }
        internal DbSet<Order> OrdersInternal { get; set; }
        internal DbSet<ShoppingCart> CartsInternal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCartItem>(item =>
            {
                item.ToTable("ShoppingCartItems");
                item.Property<int>("ShoppingCartId");
            });

            modelBuilder.Entity<ShoppingCart>(cart =>
            {
                cart.ToTable("ShoppingCarts");
            });

            
            modelBuilder.Entity<OrderLine>(orderline =>
            {
                orderline.Property<Guid>("OrderId");

                orderline.HasKey(ol => ol.Id);
                orderline.ToTable("OrderLines");
            });

            modelBuilder.Entity<OrderDiscount>(discount =>
            {
                discount.Property<Guid>("Id");
                discount.HasKey("Id");

                discount.Property<Guid>("OrderId");
                discount.ToTable("OrderDiscounts");
            });

            modelBuilder.Entity<LineDiscount>(discount =>
            {
                discount.Property<Guid>("Id");
                discount.HasKey("Id");

                discount.Property<Guid>("OrderLineId");
                discount.ToTable("LineDiscounts");
            });

            modelBuilder.Entity<Jewelry>(jewelry =>
            {
                jewelry.HasKey(j => j.Id);
                jewelry.ToTable("Jewelry");
            });

            modelBuilder.Entity<Order>()
                .HasMany(p => p.OrderLines)
                .WithOne()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey("OrderId");

            modelBuilder.Entity<OrderLine>()
                .HasMany(l => l.EffectiveDiscounts)
                .WithOne()
                .HasPrincipalKey(l => l.Id)
                .HasForeignKey("OrderLineId");

            modelBuilder.Entity<Order>()
                .HasMany(l => l.EffectiveDiscounts)
                .WithOne()
                .HasPrincipalKey(l => l.Id)
                .HasForeignKey("OrderId");

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(c => c.ContentsInternal)
                .WithOne()
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey("ShoppingCartId");
        }
    }
}
