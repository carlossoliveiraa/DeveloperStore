using AutoMapper;
using DeveloperStore.Sales.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Sales.Application.Queries.GetSaleById
{
    public sealed class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleResponseDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleResponseDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale is null)
                return null!; // ou lançar uma exceção se preferir

            return _mapper.Map<SaleResponseDto>(sale);
        }
    }
}
