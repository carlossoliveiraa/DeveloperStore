using DeveloperStore.Sales.Domain.Discounts;
using DeveloperStore.Sales.Domain.Strategies;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.UnitTests.Domain.Discounts;

public class DiscountResolverTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Resolve_ShouldReturn_NoDiscount_WhenQuantity_IsLessThan4(int quantity)
    {
        var price = 100m;

        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        discount.Should().Be(0m);
        strategy.Should().BeOfType<NoDiscount>();
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void Resolve_ShouldReturn_TenPercentDiscount_WhenQuantity_IsBetween4And9(int quantity)
    {
        var price = 100m;

        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        discount.Should().Be(quantity * price * 0.10m);
        strategy.Should().BeOfType<TenPercentDiscount>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void Resolve_ShouldReturn_TwentyPercentDiscount_WhenQuantity_IsBetween10And20(int quantity)
    {
        var price = 100m;

        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, price);

        discount.Should().Be(quantity * price * 0.20m);
        strategy.Should().BeOfType<TwentyPercentDiscount>();
    }

    [Fact]
    public void Resolve_ShouldThrow_WhenQuantity_Exceeds20()
    {
        var act = () => DiscountResolver.Resolve(21);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 items of the same product.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Resolve_ShouldThrow_WhenQuantity_IsZeroOrNegative(int quantity)
    {
        var act = () => DiscountResolver.Resolve(quantity);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Quantity must be greater than 0*");
    }

    [Theory]
    [InlineData(1, -100)]
    [InlineData(5, -10)]
    public void Calculate_ShouldThrow_WhenUnitPrice_IsNegative(int quantity, decimal unitPrice)
    {
        var strategy = DiscountResolver.Resolve(quantity);

        var act = () => strategy.Calculate(quantity, unitPrice);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Unit price cannot be negative*");
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(10, 0)]
    public void Calculate_ShouldReturn_Zero_WhenPriceIsZero(int quantity, decimal unitPrice)
    {
        var strategy = DiscountResolver.Resolve(quantity);
        var discount = strategy.Calculate(quantity, unitPrice);

        discount.Should().Be(0);
    }
}
