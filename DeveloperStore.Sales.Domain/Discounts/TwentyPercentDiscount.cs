using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TwentyPercentDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity >= 10 && quantity <= 20;

        public decimal Calculate(int quantity, decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            return quantity * unitPrice * 0.20m;
        }

    }
}
