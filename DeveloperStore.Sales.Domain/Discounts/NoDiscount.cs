using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class NoDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity < 4;

        public decimal Calculate(int quantity, decimal unitPrice) => 0;

    }
}
