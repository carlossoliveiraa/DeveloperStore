using AutoMapper;
using DeveloperStore.Sales.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Sales.Application.Queries.GetAllSales
{
    public sealed class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SaleListItemDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleListItemDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();

            return _mapper.Map<List<SaleListItemDto>>(sales);
        }
    }
}
