using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TenPercentDiscount : IDiscount
    {
        public decimal Calculate(int quantity, decimal price) => price * quantity * 0.10m;

    }
}
