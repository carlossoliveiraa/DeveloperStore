using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Tests.IntegrationTests.Sales;

public class CreateSaleTests
{
    private readonly SalesDbContext _context;

    public CreateSaleTests()
    {
        _context = Helpers.SalesDbContextFactory.Create();
    }

    [Fact]
    public async Task Should_Create_Sale_With_Correct_Discounts_And_Total()
    {
        var sale = new Sale("S-INT-001", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial SP");

        sale.AddItem(Guid.NewGuid(), "Produto 0%", 2, 100m);
        sale.AddItem(Guid.NewGuid(), "Produto 10%", 5, 200m);
        sale.AddItem(Guid.NewGuid(), "Produto 20%", 10, 300m);

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var result = await _context.Sales
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.SaleNumber == "S-INT-001");

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(3);
        result.TotalAmount.Should().BeApproximately(
            2 * 100 + 5 * 200 * 0.9m + 10 * 300 * 0.8m, 0.01m
        );
    }
}
