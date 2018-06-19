using BlingRus.Domain.Discounts;
using BlingRus.Domain.Shopping;
using FluentAssertions;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public class LongTextCostCalculatorTests
    {
        private readonly LongTextCostCalculator _sut = new PricingModel().LongTextCost;

        [Theory]
        [InlineData("")]
        [InlineData("0123456789")]
        [InlineData("abcdefghij")]
        public void CalculatesFreeTierCorrectly(string customization)
        {
            var line = new OrderLine("test", 1, 100, 100, customization);
            _sut.ApplyTo(line);
            line.EffectiveAdjustments.Should().BeEmpty();
        }

        [Theory]
        [InlineData("foo/bar/baz", 100)]
        [InlineData("foo/bar/baz/lur", 125)]
        [InlineData("this text is tier 1", 50)]
        [InlineData("this text is a bit longer, so tier 2", 100)]
        [InlineData("this text is even longer than the last, so tier 3", 150)]
        public void CalculatesTextChargesCorrectly(string customization, decimal expected)
        {
            var line = new OrderLine("test", 1, 100, 100, customization);
            _sut.ApplyTo(line);
            line.EffectiveAdjustments.Should().ContainSingle()
                .Which.AddedAmount.Should().Be(expected);
        }
    }
}