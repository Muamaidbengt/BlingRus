using System.Linq;
using BlingRus.Domain.Discounts;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Services;
using BlingRus.Domain.Shopping;
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
                new PricingModel(), 
                _mockMailService.Object);
        }

        public class WhenCartContains2ItemsCosting100Each : CheckoutServiceTests
        {
            private readonly Order _order;

            public WhenCartContains2ItemsCosting100Each()
            {
                var cart = new ShoppingCart(42);
                cart.Add(new ShoppingCartItem(1, JewelrySize.Medium, new Jewelry("Bracelet", Category.Bracelets, 100, "foo.jpg", "Braceletdescription", "Braceletdescription2")));
                cart.Add(new ShoppingCartItem(1, JewelrySize.Small, new Jewelry("Ring", Category.Rings, 100, "foo.jpg", "Ringdescription", "Ringdescription2")));
                _order = _checkoutService.CalculateOrder(cart).Result;
            }

            [Fact]
            public void ThenTheQuantityOrderedIs2()
            {
                _order.TotalQuantityOrdered.Should().Be(2);
            }

            [Fact]
            public void ThenTheShippingCostIs108()
            {
                _order.TotalShippingCost.Should().Be(36 * 2);
            }

            [Fact]
            public void ThenTheGoodsValueIs300()
            {
                _order.TotalGoodsValue.Should().Be(2 * 100);
            }

            [Fact]
            public void ThenTheVatForEachItemIs25()
            {
                foreach (var line in _order.OrderLines)
                    line.EffectiveAdjustments.Should().Contain(adjustment => adjustment.AddedAmount == 25 && adjustment.Description.Contains("tax"));
            }

            [Fact]
            public void ThenTheShippingCostShouldNotBeDiscounted()
            {
                _order.EffectiveAdjustments.Should().BeEmpty();
            }

            [Fact]
            public void ThenNoItemShouldBeFree()
            {
                foreach(var orderline in _order.OrderLines)
                    orderline.EffectiveAdjustments.Should().NotContain(d => d.DiscountedAmount == 100);
            }
        }

        public class WhenCartContains7ItemsCosting100Each : CheckoutServiceTests
        {
            private readonly Order _order;

            public WhenCartContains7ItemsCosting100Each()
            {
                var cart = new ShoppingCart(42);
                cart.Add(new ShoppingCartItem(7, JewelrySize.Medium, new Jewelry("Ring", Category.Rings, 100, "foo.jpg", "Ringdescription", "Ringdescription2")));
                _order = _checkoutService.CalculateOrder(cart).Result;
            }

            [Fact]
            public void ThenTheQuantityOrderedIs7()
            {
                _order.TotalQuantityOrdered.Should().Be(7);
            }

            [Fact]
            public void ThenTheShippingCostIs252()
            {
                _order.TotalShippingCost.Should().Be(36 * 7);
            }

            [Fact]
            public void ThenTheGoodsValueIs700()
            {
                _order.TotalGoodsValue.Should().Be(7 * 100);
            }

            [Fact]
            public void ThenTheShippingCostShouldBeDiscounted()
            {
                _order.EffectiveAdjustments.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 36 * 7);
            }

            [Fact]
            public void ThenThereShouldBeA10PercentDiscount()
            {
                _order.EffectiveAdjustments.Should()
                    .ContainSingle(discount => discount.DiscountedAmount == 700m / 10);
            }

            [Fact]
            public void ThenOneItemShouldBeFree()
            {
                _order.OrderLines.Should()
                    .HaveCount(1)
                    .And.Subject.First().EffectiveAdjustments.Should()
                    .Contain(discount => discount.DiscountedAmount == 100);
            }
        }
    }
}