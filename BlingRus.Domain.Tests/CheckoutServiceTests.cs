using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public abstract class CheckoutServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContext;
        private readonly Mock<IShoppingContext> _mockShoppingContext;
        private readonly CheckoutService _checkoutService;

        protected CheckoutServiceTests()
        {
            _mockHttpContext = new Mock<IHttpContextAccessor>();
            _mockShoppingContext = new Mock<IShoppingContext>();
            _checkoutService = new CheckoutService(_mockHttpContext.Object, _mockShoppingContext.Object);
        }

        public class WhenCartContains3ItemsWithoutText : CheckoutServiceTests
        {
            private readonly ShoppingCart _cart;
            private readonly Order _order;

            public WhenCartContains3ItemsWithoutText()
            {
                _cart = new ShoppingCart(42);
                _cart.Add(new ShoppingCartItem(1, JewelrySize.Medium, new Jewelry("Bracelet", "foo.jpg")));
                _cart.Add(new ShoppingCartItem(2, JewelrySize.Medium, new Jewelry("Necklace", "foo.jpg")));
                _order = _checkoutService.CalculateOrder(_cart);
            }

            [Fact]
            public void ThenTheAmountOrderedIs3()
            {
                _order.TotalAmountOrdered.Should().Be(3);
            }

            [Fact]
            public void ThenTheShippingCostIs108()
            {
                _order.TotalShippingCost.Should().Be(36 * 3);
            }

            [Fact]
            public void ThenTheGoodsValueIs300()
            {
                _order.TotalShippingCost.Should().Be(3 * 100);
            }

            [Fact]
            public void ThenThereAreNoDiscounts()
            {
                _order.EffectiveDiscounts.Should().BeEmpty();
            }
        }

        public class WhenCartContains11ItemsWithoutText : CheckoutServiceTests
        {
            private readonly ShoppingCart _cart;
            private readonly Order _order;

            public WhenCartContains11ItemsWithoutText()
            {
                _cart = new ShoppingCart(42);
                _cart.Add(new ShoppingCartItem(11, JewelrySize.Medium, new Jewelry("Bracelet", "foo.jpg")));
                _order = _checkoutService.CalculateOrder(_cart);
            }

            [Fact]
            public void ThenTheAmountOrderedIs11()
            {
                _order.TotalAmountOrdered.Should().Be(11);
            }

            [Fact]
            public void ThenTheShippingCostIs108()
            {
                _order.TotalShippingCost.Should().Be(36 * 11);
            }

            [Fact]
            public void ThenTheGoodsValueIs1100()
            {
                _order.TotalShippingCost.Should().Be(11 * 100);
            }

            [Fact]
            public void ThenTheShippingCostShouldBeDiscounted()
            {
                _order.EffectiveDiscounts.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 36 * 11);
            }

            [Fact]
            public void ThenThereShouldBeA10PercentDiscount()
            {
                _order.EffectiveDiscounts.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 1100m / 10);
            }

            [Fact]
            public void ThenThereShouldBeABogofDiscount()
            {
                _order.OrderLines.Should()
                    .HaveCount(1)
                    .And.Subject.First().EffectiveDiscounts.Should()
                    .HaveCount(1)
                    .And.ContainSingle(discount => discount.DiscountedAmount == 100);
            }
        }
    }
}