using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TenPercentDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity >= 4 && quantity < 10;

        public decimal Calculate(int quantity, decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            return quantity * unitPrice * 0.10m;
        }
    }
}
