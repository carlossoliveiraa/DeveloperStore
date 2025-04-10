using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.UnitOfWork;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Tests.UnitTests.Repositories;

public class SaleRepositoryTests
{
    private static IUnitOfWork CreateUnitOfWork()
    {
        var dbName = $"SalesDbTest-{Guid.NewGuid()}";
        var options = new DbContextOptionsBuilder<SalesDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new SalesDbContext(options);
        return new UnitOfWork(context);
    }

    [Fact]
    public async Task Should_Add_Sale_And_Retrieve_By_Id()
    {
        // Arrange
        using var uow = CreateUnitOfWork();
        var repository = uow.Repository<Sale>();

        var sale = new Sale(
            saleNumber: "TEST-001",
            saleDate: DateTime.UtcNow,
            customerId: Guid.NewGuid(),
            customerName: "Carlos Oliveira",
            branchId: Guid.NewGuid(),
            branchName: "Unidade Central"
        );
        sale.AddItem(Guid.NewGuid(), "Produto A", 5, 100m);

        // Act
        await repository.AddAsync(sale);
        await uow.CommitAsync();

        var result = await repository.GetAsync(x => x.Id == sale.Id);

        // Assert
        result.Should().NotBeNull();
        result!.SaleNumber.Should().Be("TEST-001");
        result.Items.Should().HaveCount(1);
    }

    [Fact]
    public async Task Should_Update_Sale()
    {
        // Arrange
        using var uow = CreateUnitOfWork();
        var repository = uow.Repository<Sale>();

        var sale = new Sale(
            saleNumber: "UPDATE-001",
            saleDate: DateTime.UtcNow,
            customerId: Guid.NewGuid(),
            customerName: "Cliente Original",
            branchId: Guid.NewGuid(),
            branchName: "Filial A"
        );

        await repository.AddAsync(sale);
        await uow.CommitAsync();

        // Act
        sale.Cancel(); // método do domínio
        repository.Update(sale);
        await uow.CommitAsync();

        var updated = await repository.GetAsync(x => x.Id == sale.Id);

        // Assert
        updated.Should().NotBeNull();
        updated!.IsCancelled.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Delete_Sale()
    {
        // Arrange
        using var uow = CreateUnitOfWork();
        var repository = uow.Repository<Sale>();

        var sale = new Sale(
            saleNumber: "DELETE-001",
            saleDate: DateTime.UtcNow,
            customerId: Guid.NewGuid(),
            customerName: "Carlos",
            branchId: Guid.NewGuid(),
            branchName: "Filial B"
        );

        await repository.AddAsync(sale);
        await uow.CommitAsync();

        // Act
        repository.Remove(sale);
        await uow.CommitAsync();

        var deleted = await repository.GetAsync(x => x.Id == sale.Id);

        // Assert
        deleted.Should().BeNull();
    }
}
