using MediatR;

namespace DeveloperStore.Sales.Application.Queries.GetSaleById
{
    public sealed record GetSaleByIdQuery(Guid SaleId) : IRequest<SaleResponseDto>;
}
