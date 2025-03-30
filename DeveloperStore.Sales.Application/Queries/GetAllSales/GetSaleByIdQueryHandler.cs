using AutoMapper;
using DeveloperStore.Sales.Application.Queries.GetSaleById;
using DeveloperStore.Sales.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Sales.Application.Queries.GetAllSales
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
            // Busca a venda no repositório
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            // Retorna null se não encontrar (ou pode lançar exceção se preferir)
            if (sale is null)
                return null!;

            // Mapeia a entidade para o DTO de resposta
            return _mapper.Map<SaleResponseDto>(sale);
        }
    }
}
