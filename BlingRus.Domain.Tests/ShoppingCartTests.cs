using BlingRus.Domain.Shopping;
using FluentAssertions;
using Xunit;

namespace BlingRus.Domain.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void NewlyCreatedCartIsEmpty()
        {
            new ShoppingCart(666).Count.Should().Be(0);
        }
    }
}