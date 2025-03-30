using DeveloperStore.Sales.Domain.Discounts.Interfaces;
using DeveloperStore.Sales.Domain.Strategies;

namespace DeveloperStore.Sales.Domain.Discounts
{
    public sealed class DiscountResolver
    {
        public static IDiscount Resolve(int quantity)
        {
            if (quantity < 4)
                return new NoDiscount();

            if (quantity >= 4 && quantity < 10)
                return new TenPercentDiscount();

            if (quantity >= 10 && quantity <= 20)
                return new TwentyPercentDiscount();

            throw new InvalidOperationException("Cannot sell more than 20 items of the same product.");
        }
    }
}
