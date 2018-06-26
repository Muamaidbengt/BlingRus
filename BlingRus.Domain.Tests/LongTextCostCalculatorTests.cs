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
        [InlineData("a")]
        [InlineData("01234567890123456789")]
        [InlineData("abcdefghijklmnopqrst")]
        public void CalculatesFreeTierCorrectly(string customization)
        {
            var line = new OrderLine("test", 1, 100, 100, customization);
            _sut.ApplyTo(line);
            line.EffectiveAdjustments.Should().BeEmpty();
        }

        [Theory]
        [InlineData("2 linebreaks foo/bar/baz", 100)]
        [InlineData("3 linebreaks foo/bar/baz/lur", 125)]
        [InlineData("this is 27 chars, so tier 1", 50)]
        [InlineData("this is 34 chars long, so tier 2", 100)]
        [InlineData("this text is sporting 46 characters, so tier 3", 150)]
        public void CalculatesTextChargesCorrectly(string customization, decimal expected)
        {
            var line = new OrderLine("test", 1, 100, 100, customization);
            _sut.ApplyTo(line);
            line.EffectiveAdjustments.Should().ContainSingle()
                .Which.AddedAmount.Should().Be(expected);
        }

        [Fact]
        public void IncludesAFreeLinebreakForTheTwin()
        {
            var line = new OrderLine("The Twin with text \"this has a free/line break!\"", 1, 100, 100, "this has a free/line break!");
            _sut.ApplyTo(line);
            line.EffectiveAdjustments.Should().ContainSingle()
                .Which.AddedAmount.Should().Be(50, "50 for the text length, 0 for the line breaks");
        }
    }
}