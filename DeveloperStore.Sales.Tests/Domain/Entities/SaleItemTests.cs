using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.Tests.Domain.Entities
{
    /// <summary>
    /// AAA (Arrange / Act / Assert)
    /// </summary>
    public class SaleItemTests
    {
        [Fact]
        public void Should_Not_Apply_Discount_When_Quantity_Is_Less_Than_4()
        {
            //// Arrange
            var productId = Guid.NewGuid();
            var productName = "Product A";
            var quantity = 2;
            var unitPrice = 100m;

            //// Act
            var item = new SaleItem(productId, productName, quantity, unitPrice);

            //// Assert
            Assert.Equal(0m, item.Discount);
            Assert.Equal(200m, item.Total);
        }

        [Fact]
        public void Should_Apply_10_Percent_Discount_When_Quantity_Is_Between_4_And_9()
        {
            //// Arrange
            var productId = Guid.NewGuid();
            var productName = "Product A";
            var quantity = 5;
            var unitPrice = 100m;

            //// Act
            var item = new SaleItem(productId, productName, quantity, unitPrice);

            //// Assert
            Assert.Equal(50m, item.Discount); // 5 * 100 * 0.10
            Assert.Equal(450m, item.Total);
        }

        [Fact]
        public void Should_Apply_20_Percent_Discount_When_Quantity_Is_Between_10_And_20()
        {
            //// Arrange
            var productId = Guid.NewGuid();
            var productName = "Product A";
            var quantity = 15;
            var unitPrice = 100m;

            //// Act
            var item = new SaleItem(productId, productName, quantity, unitPrice);

            //// Assert
            Assert.Equal(300m, item.Discount); // 15 * 100 * 0.20
            Assert.Equal(1200m, item.Total);
        }

        [Fact]
        public void Should_Apply_20_Percent_Discount_When_Quantity_Is_Exactly_20()
        {
            //// Arrange
            var productId = Guid.NewGuid();
            var productName = "Product A";
            var quantity = 20;
            var unitPrice = 100m;

            //// Act
            var item = new SaleItem(productId, productName, quantity, unitPrice);

            //// Assert
            Assert.Equal(400m, item.Discount); // 20 * 100 * 0.20
            Assert.Equal(1600m, item.Total);
        }

        [Fact]
        public void Should_Throw_Exception_When_Quantity_Is_Greater_Than_20()
        {
            //// Arrange
            var productId = Guid.NewGuid();
            var productName = "Product A";
            var quantity = 21;
            var unitPrice = 100m;

            //// Act
            var act = () => new SaleItem(productId, productName, quantity, unitPrice);

            //// Assert
            var exception = Assert.Throws<BusinessRuleValidationException>(act);
            Assert.Equal("Cannot sell more than 20 items of the same product.", exception.Message);
        }
    }
}
