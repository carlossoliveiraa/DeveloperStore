using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Sales.Application.Commands.CreateSale
{
    public sealed class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {           
            var sale = new Sale(
                request.SaleNumber,
                DateTime.UtcNow,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );
                       
            foreach (var item in request.Items)
            {
                sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
            }
           
            await _saleRepository.AddAsync(sale);                       
            return sale.Id;
        }
    }
}