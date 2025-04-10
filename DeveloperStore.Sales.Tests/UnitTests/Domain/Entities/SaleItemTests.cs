using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.UnitTests.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void Should_Not_Apply_Discount_When_Quantity_Is_Less_Than_4()
    {
        var item = new SaleItem(Guid.NewGuid(), "Product A", 2, 100m);

        item.Discount.Should().Be(0m);
        item.Total.Should().Be(200m);
    }

    [Fact]
    public void Should_Apply_10_Percent_Discount_When_Quantity_Between_4_And_9()
    {
        var item = new SaleItem(Guid.NewGuid(), "Product A", 5, 100m);

        item.Discount.Should().Be(50m);
        item.Total.Should().Be(450m);
    }

    [Fact]
    public void Should_Apply_20_Percent_Discount_When_Quantity_Between_10_And_20()
    {
        var item = new SaleItem(Guid.NewGuid(), "Product A", 15, 100m);

        item.Discount.Should().Be(300m);
        item.Total.Should().Be(1200m);
    }

    [Fact]
    public void Should_Apply_20_Percent_Discount_When_Quantity_Is_Exactly_20()
    {
        var item = new SaleItem(Guid.NewGuid(), "Product A", 20, 100m);

        item.Discount.Should().Be(400m);
        item.Total.Should().Be(1600m);
    }

    [Fact]
    public void Should_Throw_When_Quantity_Greater_Than_20()
    {
        var act = () => new SaleItem(Guid.NewGuid(), "Product A", 21, 100m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Cannot sell more than 20 items of the same product.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_Throw_When_Quantity_Is_Zero_Or_Negative(int quantity)
    {
        var act = () => new SaleItem(Guid.NewGuid(), "Product A", quantity, 100m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Quantity must be greater than 0.");
    }

    [Fact]
    public void Should_Throw_When_UnitPrice_Is_Negative()
    {
        var act = () => new SaleItem(Guid.NewGuid(), "Product A", 5, -10m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Unit price cannot be negative.");
    }

    [Fact]
    public void Should_Calculate_ZeroTotal_When_UnitPrice_Is_Zero()
    {
        var item = new SaleItem(Guid.NewGuid(), "Product A", 10, 0m);

        item.Discount.Should().Be(0m);
        item.Total.Should().Be(0m);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_Throw_When_ProductName_Is_EmptyOrNull(string? productName)
    {
        var act = () => new SaleItem(Guid.NewGuid(), productName!, 5, 100m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Product name is required.");
    }

    [Fact]
    public void Should_Throw_When_ProductId_Is_Empty()
    {
        var act = () => new SaleItem(Guid.Empty, "Product A", 5, 100m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Product ID is required.");
    }
}
