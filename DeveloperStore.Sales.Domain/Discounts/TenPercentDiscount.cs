using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TenPercentDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity >= 4 && quantity < 10;

        public decimal Calculate(int quantity, decimal unitPrice)
        {
            var total = quantity * unitPrice;
            return total * 0.10m;
        }

    }
}
