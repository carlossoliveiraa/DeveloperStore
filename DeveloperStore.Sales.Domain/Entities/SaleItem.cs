using DeveloperStore.Sales.Domain.Exceptions;
using DeveloperStore.Sales.Domain.Discounts;

namespace DeveloperStore.Sales.Domain.Entities
{
    public sealed class SaleItem : BaseEntity
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal Discount { get; }
        public decimal Total => (UnitPrice * Quantity) - Discount;

        public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (productId == Guid.Empty)
                throw new BusinessRuleValidationException("Product ID is required.");

            if (string.IsNullOrWhiteSpace(productName))
                throw new BusinessRuleValidationException("Product name is required.");

            if (quantity <= 0)
                throw new BusinessRuleValidationException("Quantity must be greater than 0.");

            if (quantity > 20)
                throw new BusinessRuleValidationException("Cannot sell more than 20 items of the same product.");

            if (unitPrice < 0)
                throw new BusinessRuleValidationException("Unit price cannot be negative.");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            var discount = DiscountResolver.Resolve(quantity);
            Discount = discount.Calculate(quantity, unitPrice);
        }
    }
}
