using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Create_ShouldInitializeSale_WithValidData()
    {
        // Arrange
        var saleNumber = "20250327-001";
        var saleDate = DateTime.UtcNow;
        var customerId = Guid.NewGuid();
        var customerName = "Carlos Oliveira";
        var branchId = Guid.NewGuid();
        var branchName = "Alfenas MG";

        // Act
        var sale = new Sale(saleNumber, saleDate, customerId, customerName, branchId, branchName);

        // Assert
        sale.SaleNumber.Should().Be(saleNumber);
        sale.SaleDate.Should().Be(saleDate);
        sale.CustomerId.Should().Be(customerId);
        sale.CustomerName.Should().Be(customerName);
        sale.BranchId.Should().Be(branchId);
        sale.BranchName.Should().Be(branchName);
        sale.IsCancelled.Should().BeFalse();
        sale.Items.Should().BeEmpty();
        sale.TotalAmount.Should().Be(0m);
    }

    [Fact]
    public void Create_ShouldThrow_WhenSaleNumberIsEmpty()
    {
        // Act
        var act = () => new Sale("", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        // Assert
        act.Should()
           .Throw<BusinessRuleValidationException>()
           .WithMessage("Sale number is required.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenCustomerIdIsEmpty()
    {
        // Act
        var act = () => new Sale("S-001", DateTime.UtcNow, Guid.Empty, "Client", Guid.NewGuid(), "Branch");

        // Assert
        act.Should()
           .Throw<BusinessRuleValidationException>()
           .WithMessage("Customer ID is required.");
    }

    [Fact]
    public void AddItem_ShouldUpdateItemsAndTotalAmount()
    {
        // Arrange
        var sale = new Sale("S-002", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        var productId = Guid.NewGuid();
        var productName = "Mouse";
        var quantity = 5;
        var unitPrice = 100m;

        // Act
        sale.AddItem(productId, productName, quantity, unitPrice);

        // Assert
        sale.Items.Should().HaveCount(1);
        var item = sale.Items.First();
        item.Discount.Should().Be(50m); // 5 * 100 * 0.10
        sale.TotalAmount.Should().Be(450m);
    }

    [Fact]
    public void Cancel_ShouldMarkSaleAsCancelled()
    {
        // Arrange
        var sale = new Sale("S-003", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        // Act
        sale.Cancel();

        // Assert
        sale.IsCancelled.Should().BeTrue();
    }
}
