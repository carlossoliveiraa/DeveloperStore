using DeveloperStore.Sales.Domain.Discounts.Interfaces;

namespace DeveloperStore.Sales.Domain.Strategies
{
    public sealed class NoDiscount : IDiscount
    {
        public decimal Calculate(int quantity, decimal price) => 0;    
        
    }
}
