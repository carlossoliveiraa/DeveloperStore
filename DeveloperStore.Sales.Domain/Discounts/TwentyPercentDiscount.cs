using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class TwentyPercentDiscount : IDiscount
    {
        public decimal Calculate(int quantity, decimal price) => quantity * price * 0.20m;

    }
}
