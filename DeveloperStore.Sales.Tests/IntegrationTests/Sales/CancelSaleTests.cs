using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using static DeveloperStore.Sales.Tests.IntegrationTests.Helpers.SalesDbContextFactory;

public class CancelSaleTests
{
    private readonly SalesDbContext _context;

    public CancelSaleTests()
    {
        _context = Create();
    }

    [Fact]
    public async Task Should_Cancel_Sale_Correctly()
    {
        var sale = new Sale("S-CANCEL-001", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial");
        sale.AddItem(Guid.NewGuid(), "Mouse", 2, 100m);
        sale.Cancel();

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var result = await _context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == "S-CANCEL-001");

        result.Should().NotBeNull();
        result!.IsCancelled.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_When_Cancelling_Twice()
    {
        var sale = new Sale("S-CANCEL-002", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial");
        sale.Cancel();

        Action act = () => sale.Cancel();

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Sale is already cancelled.");
    }

    [Fact]
    public void Should_Throw_When_Adding_Item_To_Cancelled_Sale()
    {
        var sale = new Sale("S-CANCEL-003", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial");
        sale.Cancel();

        Action act = () => sale.AddItem(Guid.NewGuid(), "Mouse", 3, 100m);

        act.Should().Throw<BusinessRuleValidationException>()
           .WithMessage("Cannot add items to a cancelled sale.");
    }
}
