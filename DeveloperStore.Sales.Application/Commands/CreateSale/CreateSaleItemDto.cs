namespace DeveloperStore.Sales.Application.Commands.CreateSale
{
    public sealed record CreateSaleItemDto(
     Guid ProductId,
     string ProductName,
     int Quantity,
     decimal UnitPrice
 );
}
