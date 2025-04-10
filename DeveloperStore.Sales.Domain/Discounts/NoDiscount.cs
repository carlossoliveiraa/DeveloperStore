using DeveloperStore.Sales.Domain.Discounts.Interfaces;
using System;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class NoDiscount : IDiscount
    {
        public bool IsApplicable(int quantity) => quantity < 4;

        public decimal Calculate(int quantity, decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            return 0m;
        }
    }
}
