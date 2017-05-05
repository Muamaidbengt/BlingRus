using System.Linq;
using FluentAssertions;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public class SuperTests
    {
        [Fact]
        public void CheckoutService()
        {
            var svc = new CheckoutService();
            var cart = new ShoppingCart();
            cart.Add(new ShoppingCartItem(1, new Jewelry("Bracelet", JewelrySize.Medium, "foo.jpg")));
            cart.Add(new ShoppingCartItem(2, new Jewelry("Necklace", JewelrySize.Medium, "foo.jpg")));

            var order = svc.CalculateOrder(cart);
            order.TotalAmountOrdered.Should().Be(3);
            order.TotalGoodsValue.Should().Be(300);
            order.TotalShippingCost.Should().Be(36 * 3);
        }

        [Fact]
        public void CheckoutService2()
        {
            var svc = new CheckoutService();
            var cart = new ShoppingCart();
            cart.Add(new ShoppingCartItem(11, new Jewelry("Bracelet", JewelrySize.Medium, "foo.jpg")));

            var order = svc.CalculateOrder(cart);
            order.TotalAmountOrdered.Should().Be(11);
            order.TotalGoodsValue.Should().Be(1100);
            order.TotalShippingCost.Should().Be(36 * 11);
            order.EffectiveDiscounts.Should()
                .HaveCount(2)
                .And.ContainSingle(discount => discount.DiscountedAmount == 36 * 11)
                .And.ContainSingle(discount => discount.DiscountedAmount == 1100m / 10);
            order.OrderLines.Should()
                .HaveCount(1)
                .And.Subject.First().EffectiveDiscounts.Should()
                .HaveCount(1)
                .And.ContainSingle(discount => discount.DiscountedAmount == 100); // Bugg
        }
    }
}
