using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.UnitTests.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Create_ShouldInitializeSale_WithValidData()
    {
        var sale = new Sale("S-001", DateTime.UtcNow, Guid.NewGuid(), "Carlos Oliveira", Guid.NewGuid(), "Alfenas MG");

        sale.SaleNumber.Should().Be("S-001");
        sale.IsCancelled.Should().BeFalse();
        sale.Items.Should().BeEmpty();
        sale.TotalAmount.Should().Be(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_ShouldThrow_WhenSaleNumberIsInvalid(string? saleNumber)
    {
        var act = () => new Sale(saleNumber!, DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Sale number is required.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenCustomerIdIsEmpty()
    {
        var act = () => new Sale("S-001", DateTime.UtcNow, Guid.Empty, "Client", Guid.NewGuid(), "Branch");

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Customer ID is required.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenCustomerNameIsEmpty()
    {
        var act = () => new Sale("S-001", DateTime.UtcNow, Guid.NewGuid(), "", Guid.NewGuid(), "Branch");

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Customer name is required.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenBranchIdIsEmpty()
    {
        var act = () => new Sale("S-001", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.Empty, "Branch");

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Branch ID is required.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenBranchNameIsEmpty()
    {
        var act = () => new Sale("S-001", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "");

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Branch name is required.");
    }

    [Fact]
    public void AddItem_ShouldUpdateTotal_ForMultipleItems()
    {
        var sale = new Sale("S-002", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial");

        sale.AddItem(Guid.NewGuid(), "Produto A", 4, 100); // 10% desc => 360
        sale.AddItem(Guid.NewGuid(), "Produto B", 10, 200); // 20% desc => 1600

        sale.Items.Should().HaveCount(2);
        sale.TotalAmount.Should().Be(1960m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddItem_ShouldThrow_WhenQuantityIsInvalid(int quantity)
    {
        var sale = new Sale("S-003", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        var act = () => sale.AddItem(Guid.NewGuid(), "Produto X", quantity, 100);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Quantity must be greater than 0.");
    }

    [Fact]
    public void AddItem_ShouldThrow_WhenPriceIsNegative()
    {
        var sale = new Sale("S-004", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        var act = () => sale.AddItem(Guid.NewGuid(), "Produto Y", 1, -10);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Unit price cannot be negative.");
    }

    [Fact]
    public void Cancel_ShouldMarkSaleAsCancelled()
    {
        var sale = new Sale("S-005", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

        sale.Cancel();

        sale.IsCancelled.Should().BeTrue();
    }

    [Fact]
    public void Cancel_ShouldThrow_WhenSaleAlreadyCancelled()
    {
        var sale = new Sale("S-006", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");
        sale.Cancel();

        var act = () => sale.Cancel();

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Sale is already cancelled.");
    }

    [Fact]
    public void AddItem_ShouldThrow_WhenSaleIsCancelled()
    {
        var sale = new Sale("S-007", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");
        sale.Cancel();

        var act = () => sale.AddItem(Guid.NewGuid(), "Produto", 5, 100);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Cannot add items to a cancelled sale.");
    }
}
