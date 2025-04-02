namespace DeveloperStore.Sales.Domain.Discounts.Interfaces
{
    public interface IDiscount
    {
        bool IsApplicable(int quantity);
        decimal Calculate(int quantity, decimal unitPrice);
    }
}
