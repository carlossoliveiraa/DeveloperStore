using MediatR;

namespace DeveloperStore.Sales.Application.Queries.GetAllSales
{
    public sealed record GetAllSalesQuery() : IRequest<List<SaleListItemDto>>;
}
