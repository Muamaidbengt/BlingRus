using System.Linq;
using BlingRus.Domain.Discounts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public abstract class CheckoutServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContext = new Mock<IHttpContextAccessor>();
        private readonly Mock<IShoppingContext> _mockShoppingContext = new Mock<IShoppingContext>();
        private readonly Mock<IMailService> _mockMailService = new Mock<IMailService>();
        private readonly CheckoutService _checkoutService;

        protected CheckoutServiceTests()
        {
            _checkoutService = new CheckoutService(
                _mockHttpContext.Object, 
                _mockShoppingContext.Object, 
                new PriceAdjustmentModel(), 
                _mockMailService.Object);
        }

        public class WhenCartContains3ItemsWithoutText : CheckoutServiceTests
        {
            private readonly ShoppingCart _cart;
            private readonly Order _order;

            public WhenCartContains3ItemsWithoutText()
            {
                _cart = new ShoppingCart(42);
                _cart.Add(new ShoppingCartItem(1, JewelrySize.Medium, new Jewelry("Bracelet", Category.Bracelets, 100, "foo.jpg", "Braceletdescription", "Braceletdescription2")));
                _cart.Add(new ShoppingCartItem(2, JewelrySize.Medium, new Jewelry("Necklace", Category.Necklaces, 100, "foo.jpg", "Necklacedescription", "Necklacedescription2")));
                _order = _checkoutService.CalculateOrder(_cart);
            }

            [Fact]
            public void ThenTheQuantityOrderedIs3()
            {
                _order.TotalQuantityOrdered.Should().Be(3);
            }

            [Fact]
            public void ThenTheShippingCostIs108()
            {
                _order.TotalShippingCost.Should().Be(36 * 3);
            }

            [Fact]
            public void ThenTheGoodsValueIs300()
            {
                _order.TotalGoodsValue.Should().Be(3 * 100);
            }

            [Fact]
            public void ThenTheShippingCostShouldBeDiscounted()
            {
                _order.EffectiveAdjustments.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 36 * 3);
            }
        }

        public class WhenCartContains11ItemsWithoutText : CheckoutServiceTests
        {
            private readonly ShoppingCart _cart;
            private readonly Order _order;

            public WhenCartContains11ItemsWithoutText()
            {
                _cart = new ShoppingCart(42);
                _cart.Add(new ShoppingCartItem(11, JewelrySize.Medium, new Jewelry("Bracelet", Category.Bracelets, 100, "foo.jpg", "Braceletdescription", "Braceletdescription2")));
                _order = _checkoutService.CalculateOrder(_cart);
            }

            [Fact]
            public void ThenTheQuantityOrderedIs11()
            {
                _order.TotalQuantityOrdered.Should().Be(11);
            }

            [Fact]
            public void ThenTheShippingCostIs108()
            {
                _order.TotalShippingCost.Should().Be(36 * 11);
            }

            [Fact]
            public void ThenTheGoodsValueIs1100()
            {
                _order.TotalGoodsValue.Should().Be(11 * 100);
            }

            [Fact]
            public void ThenTheShippingCostShouldBeDiscounted()
            {
                _order.EffectiveAdjustments.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 36 * 11);
            }

            [Fact]
            public void ThenThereShouldBeA10PercentDiscount()
            {
                _order.EffectiveAdjustments.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 1100m / 10);
            }

            [Fact]
            public void ThenThereShouldBeABogofDiscount()
            {
                _order.OrderLines.Should()
                    .HaveCount(1)
                    .And.Subject.First().EffectiveAdjustments.Should()
                    .Contain(discount => discount.DiscountedAmount == 100);
            }
        }
    }
}