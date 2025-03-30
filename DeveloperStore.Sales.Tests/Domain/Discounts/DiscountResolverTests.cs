using DeveloperStore.Sales.Domain.Discounts;

namespace DeveloperStore.Sales.Tests.Domain.Discounts
{
    public class DiscountResolverTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Resolve_NoDiscount_When_Quantity_Is_Less_Than_4(int quantity)
        {
            //// Arrange
            var price = 100m;

            //// Act
            var strategy = DiscountResolver.Resolve(quantity);
            var discount = strategy.Calculate(quantity, price);

            //// Assert
            Assert.Equal(0m, discount);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(9)]
        public void Should_Resolve_TenPercentDiscount_When_Quantity_Is_Between_4_And_9(int quantity)
        {
            //// Arrange
            var price = 100m;

            //// Act
            var strategy = DiscountResolver.Resolve(quantity);
            var discount = strategy.Calculate(quantity, price);

            //// Assert
            var expected = quantity * price * 0.10m;
            Assert.Equal(expected, discount);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void Should_Resolve_TwentyPercentDiscount_When_Quantity_Is_Between_10_And_20(int quantity)
        {
            //// Arrange
            var price = 100m;

            //// Act
            var strategy = DiscountResolver.Resolve(quantity);
            var discount = strategy.Calculate(quantity, price);

            //// Assert
            var expected = quantity * price * 0.20m;
            Assert.Equal(expected, discount);
        }

        [Fact]
        public void Should_Throw_When_Quantity_Exceeds_20()
        {
            //// Arrange
            var quantity = 21;

            //// Act
            var act = () => DiscountResolver.Resolve(quantity);

            //// Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Cannot sell more than 20 items of the same product.", exception.Message);
        }
    }
}