using System;
using System.Linq;
using BlingRus.Domain.Discounts;
using Microsoft.EntityFrameworkCore;

namespace BlingRus.Domain
{
    public class ShoppingContext : DbContext, IShoppingContext
    {
        //private readonly string _filename;
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
            //_filename = filename;
        }

        public IQueryable<ShoppingCart> Carts => CartsInternal.Include(c => c.Contents);

        public void Add(Order order)
        {
            Orders.Add(order);
        }

        public void Add(ShoppingCart cart)
        {
            CartsInternal.Add(cart);
        }

        public void Save()
        {
            SaveChanges();
        }

        protected DbSet<Order> Orders { get; set; }
        protected DbSet<ShoppingCart> CartsInternal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlite($"Filename={_filename}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCarts");
            modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItems");
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
        }
    }
}
