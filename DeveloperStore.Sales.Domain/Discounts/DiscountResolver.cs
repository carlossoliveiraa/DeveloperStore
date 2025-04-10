using DeveloperStore.Sales.Domain.Discounts.Interfaces;
using DeveloperStore.Sales.Domain.Strategies;

namespace DeveloperStore.Sales.Domain.Discounts
{
    /// <summary>
    /// Resolves the appropriate discount strategy based on product quantity.
    /// Applies the Strategy Pattern to isolate discount logic.
    /// </summary>
    public sealed class DiscountResolver
    {
        private static readonly List<IDiscount> _strategies = new()
        {
            new NoDiscount(),
            new TenPercentDiscount(),
            new TwentyPercentDiscount()
        };

        /// <summary>
        /// Selects the first strategy that matches the quantity condition.
        /// </summary>
        /// <param name="quantity">The quantity of products being purchased.</param>
        /// <returns>The applicable IDiscount implementation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when quantity is zero or negative.</exception>
        /// <exception cref="InvalidOperationException">Thrown when quantity exceeds the allowed maximum.</exception>
        public static IDiscount Resolve(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");

            var strategy = _strategies.FirstOrDefault(s => s.IsApplicable(quantity));

            return strategy ?? throw new InvalidOperationException("Cannot sell more than 20 items of the same product.");
        }
    }
}
