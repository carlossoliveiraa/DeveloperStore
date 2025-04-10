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
        // Arrange
        var sale = new Sale("S-INT-001", DateTime.UtcNow, Guid.NewGuid(), "Carlos", Guid.NewGuid(), "Filial SP");

        var product1Id = Guid.NewGuid();
        var product2Id = Guid.NewGuid();
        var product3Id = Guid.NewGuid();

        sale.AddItem(product1Id, "Produto 0%", 2, 100m);    // No discount
        sale.AddItem(product2Id, "Produto 10%", 5, 200m);   // 10% discount
        sale.AddItem(product3Id, "Produto 20%", 10, 300m);  // 20% discount

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        // Act
        var result = await _context.Sales
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.SaleNumber == "S-INT-001");

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(3);

        var item0 = result.Items.First(i => i.ProductId == product1Id);
        var item1 = result.Items.First(i => i.ProductId == product2Id);
        var item2 = result.Items.First(i => i.ProductId == product3Id);

        // Item 0: no discount
        item0.Discount.Should().Be(0m);
        item0.Total.Should().Be(2 * 100);

        // Item 1: 10% discount
        item1.Discount.Should().Be(5 * 200 * 0.10m); // 100.00
        item1.Total.Should().Be(5 * 200 - item1.Discount); // 1000 - 100 = 900

        // Item 2: 20% discount
        item2.Discount.Should().Be(10 * 300 * 0.20m); // 600.00
        item2.Total.Should().Be(10 * 300 - item2.Discount); // 3000 - 600 = 2400

        // Total geral
        result.TotalAmount.Should().BeApproximately(
            item0.Total + item1.Total + item2.Total, 0.01m
        );
    }
}
