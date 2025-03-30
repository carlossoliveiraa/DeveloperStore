using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Infrastructure.Repositories;
using DeveloperStore.Sales.Infrastructure.TestUtils;
using FluentAssertions;

namespace DeveloperStore.Sales.Tests.Repositories
{
    public class SaleRepositoryTests
    {
        [Fact]
        public async Task Should_Add_Sale_And_Retrieve_By_Id()
        {
            //// Arrange
            var dbName = Guid.NewGuid().ToString();
            var context = TestSalesDbContextFactory.CreateInMemoryContext(dbName);
            var repository = new SaleRepository(context);

            var sale = new Sale(
                saleNumber: "TEST-001",
                saleDate: DateTime.UtcNow,
                customerId: Guid.NewGuid(),
                customerName: "Carlos Oliveira",
                branchId: Guid.NewGuid(),
                branchName: "Unidade Central"
            );

            sale.AddItem(Guid.NewGuid(), "Produto A", 5, 100m);

            //// Act
            await repository.AddAsync(sale);
            var result = await repository.GetByIdAsync(sale.Id);

            //// Assert
            result.Should().NotBeNull();
            result!.SaleNumber.Should().Be("TEST-001");
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task Should_Return_All_Sales()
        {
            //// Arrange
            var dbName = Guid.NewGuid().ToString();
            var context = TestSalesDbContextFactory.CreateInMemoryContext(dbName);
            var repository = new SaleRepository(context);

            for (int i = 1; i <= 3; i++)
            {
                var sale = new Sale(
                    saleNumber: $"TEST-00{i}",
                    saleDate: DateTime.UtcNow,
                    customerId: Guid.NewGuid(),
                    customerName: $"Cliente {i}",
                    branchId: Guid.NewGuid(),
                    branchName: "Filial A"
                );

                sale.AddItem(Guid.NewGuid(), $"Produto {i}", 2, 100m);
                await repository.AddAsync(sale);
            }

            //// Act
            var allSales = await repository.GetAllAsync();

            //// Assert
            allSales.Should().HaveCount(3);
        }

        [Fact]
        public async Task Should_Update_Sale()
        {
            //// Arrange
            var dbName = Guid.NewGuid().ToString();
            var context = TestSalesDbContextFactory.CreateInMemoryContext(dbName);
            var repository = new SaleRepository(context);

            var sale = new Sale(
                saleNumber: "UPDATE-001",
                saleDate: DateTime.UtcNow,
                customerId: Guid.NewGuid(),
                customerName: "Cliente Original",
                branchId: Guid.NewGuid(),
                branchName: "Filial A"
            );

            await repository.AddAsync(sale);

            //// Act
            sale.Cancel(); // Método do domínio
            await repository.UpdateAsync(sale);
            var updatedSale = await repository.GetByIdAsync(sale.Id);

            //// Assert
            updatedSale.Should().NotBeNull();
            updatedSale!.IsCancelled.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Delete_Sale()
        {
            //// Arrange
            var dbName = Guid.NewGuid().ToString();
            var context = TestSalesDbContextFactory.CreateInMemoryContext(dbName);
            var repository = new SaleRepository(context);

            var sale = new Sale(
                saleNumber: "DELETE-001",
                saleDate: DateTime.UtcNow,
                customerId: Guid.NewGuid(),
                customerName: "Carlos",
                branchId: Guid.NewGuid(),
                branchName: "Filial B"
            );

            await repository.AddAsync(sale);

            //// Act
            await repository.DeleteAsync(sale.Id);
            var deletedSale = await repository.GetByIdAsync(sale.Id);

            //// Assert
            deletedSale.Should().BeNull();
        }
    }
}
