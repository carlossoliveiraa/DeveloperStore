using DeveloperStore.Sales.Domain.Discounts;
using DeveloperStore.Sales.Domain.Strategies;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.Domain.Discounts;

public class DiscountResolverTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Resolve_ShouldReturn_NoDiscount_WhenQuantity_IsLessThan4(int quantity)
    {
        // Arrange
        var price = 100m;

        // Act
        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        // Assert
        discount.Should().Be(0m);
        strategy.Should().BeOfType<NoDiscount>();
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void Resolve_ShouldReturn_TenPercentDiscount_WhenQuantity_IsBetween4And9(int quantity)
    {
        // Arrange
        var price = 100m;

        // Act
        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        // Assert
        discount.Should().Be(quantity * price * 0.10m);
        strategy.Should().BeOfType<TenPercentDiscount>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void Resolve_ShouldReturn_TwentyPercentDiscount_WhenQuantity_IsBetween10And20(int quantity)
    {
        // Arrange
        var price = 100m;

        // Act
        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        // Assert
        discount.Should().Be(quantity * price * 0.20m);
        strategy.Should().BeOfType<TwentyPercentDiscount>();
    }

    [Fact]
    public void Resolve_ShouldThrow_WhenQuantity_Exceeds20()
    {
        // Arrange
        var quantity = 21;

        // Act
        var act = () => DiscountResolver.Resolve(quantity);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 items of the same product.");
    }
}
