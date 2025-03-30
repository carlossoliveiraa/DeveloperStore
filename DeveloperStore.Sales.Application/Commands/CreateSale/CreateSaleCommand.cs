using MediatR;

namespace DeveloperStore.Sales.Application.Commands.CreateSale
{
    public sealed record CreateSaleCommand(string SaleNumber, Guid CustomerId,
                                           string CustomerName,Guid BranchId,
                                           string BranchName, List<CreateSaleItemDto> Items) : IRequest<Guid>; 
}
