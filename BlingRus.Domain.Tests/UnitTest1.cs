using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public class SuperTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContext;
        private readonly Mock<IShoppingContext> _mockShoppingContext;
        private readonly CheckoutService _checkoutService;

        public SuperTests()
        {
            _mockHttpContext = new Mock<IHttpContextAccessor>();
            _mockShoppingContext = new Mock<IShoppingContext>();
            _checkoutService = new CheckoutService(_mockHttpContext.Object, _mockShoppingContext.Object);
        }


        [Fact]
        public void CheckoutService()
        {
            var cart = new ShoppingCart();
            cart.Add(new ShoppingCartItem(1, JewelrySize.Medium, new Jewelry("Bracelet", "foo.jpg")));
            cart.Add(new ShoppingCartItem(2, JewelrySize.Medium, new Jewelry("Necklace", "foo.jpg")));

            var order = _checkoutService.CalculateOrder(cart);
            order.TotalAmountOrdered.Should().Be(3);
            order.TotalGoodsValue.Should().Be(300);
            order.TotalShippingCost.Should().Be(36 * 3);
        }

        [Fact]
        public void CheckoutService2()
        {
            var cart = new ShoppingCart();
            cart.Add(new ShoppingCartItem(11, JewelrySize.Medium, new Jewelry("Bracelet", "foo.jpg")));

            var order = _checkoutService.CalculateOrder(cart);
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
