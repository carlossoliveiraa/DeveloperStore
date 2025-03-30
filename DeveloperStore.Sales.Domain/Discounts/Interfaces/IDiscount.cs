namespace DeveloperStore.Sales.Domain.Discounts.Interfaces
{
    public interface IDiscount
    {
        decimal Calculate(int quantity, decimal price);
    }
}
