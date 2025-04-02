using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TwentyPercentDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity >= 10 && quantity <= 20;

        public decimal Calculate(int quantity, decimal unitPrice)
        {
            var total = quantity * unitPrice;
            return total * 0.20m;
        }

    }
}
