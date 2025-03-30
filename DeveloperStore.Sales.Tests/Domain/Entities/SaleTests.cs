using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.Tests.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Should_Create_Sale_With_Valid_Data()
        {
            //// Arrange
            //// Set up valid sale details
            var saleNumber = "20250327-001";
            var saleDate = DateTime.UtcNow;
            var customerId = Guid.NewGuid();
            var customerName = "Carlos Oliveira";
            var branchId = Guid.NewGuid();
            var branchName = "Alfenas MG";

            //// Act
            var sale = new Sale(saleNumber, saleDate, customerId, customerName, branchId, branchName);

            //// Assert
            Assert.Equal(saleNumber, sale.SaleNumber);
            Assert.Equal(saleDate, sale.SaleDate);
            Assert.Equal(customerId, sale.CustomerId);
            Assert.Equal(customerName, sale.CustomerName);
            Assert.Equal(branchId, sale.BranchId);
            Assert.Equal(branchName, sale.BranchName);
            Assert.False(sale.IsCancelled);
            Assert.Empty(sale.Items);
            Assert.Equal(0m, sale.TotalAmount);
        }

        [Fact]
        public void Should_Throw_When_SaleNumber_Is_Empty()
        {
            //// Arrange
            ///  Try to create a sale with an empty sale number
            var act = () => new Sale("", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

            //// Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(act);
            Assert.Equal("Sale number is required.", exception.Message);
        }

        [Fact]
        public void Should_Throw_When_CustomerId_Is_Empty()
        {
            //// Arrange
            ///  Try to create a sale with an empty customer ID
            var act = () => new Sale("S-001", DateTime.UtcNow, Guid.Empty, "Client", Guid.NewGuid(), "Branch");

            //// Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(act);
            Assert.Equal("Customer ID is required.", exception.Message);
        }

        [Fact]
        public void Should_Add_SaleItem_And_Update_TotalAmount()
        {
            //// Arrange
            ///  Create a valid sale
            var sale = new Sale("S-002", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

            var productId = Guid.NewGuid();
            var productName = "Mouse";
            var quantity = 5;
            var unitPrice = 100m;

            //// Act
            sale.AddItem(productId, productName, quantity, unitPrice);

            //// Assert
            Assert.Single(sale.Items);
            Assert.Equal(50m, sale.Items[0].Discount); // 10%
            Assert.Equal(450m, sale.TotalAmount);
        }

        [Fact]
        public void Should_Cancel_Sale()
        {
            //// Arrange
            ///  Create a valid sale
            var sale = new Sale("S-003", DateTime.UtcNow, Guid.NewGuid(), "Client", Guid.NewGuid(), "Branch");

            //// Act
            sale.Cancel();

            //// Assert
            Assert.True(sale.IsCancelled);
        }
    }
}