using AutoMapper;
using DeveloperStore.Sales.Application.Queries.GetAllSales;
using DeveloperStore.Sales.Application.Queries.GetSaleById;
using DeveloperStore.Sales.Domain.Entities;

namespace DeveloperStore.Sales.Application.Mappings
{
    public sealed class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            // Map Sale -> SaleResponseDto
            CreateMap<Sale, SaleResponseDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            // Map SaleItem -> SaleItemResponseDto
            CreateMap<SaleItem, SaleItemResponseDto>();

            CreateMap<Sale, SaleListItemDto>();
        }
    }
}
