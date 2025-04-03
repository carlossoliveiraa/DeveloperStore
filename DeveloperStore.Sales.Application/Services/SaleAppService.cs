using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.DTOs.Inputs;
using DeveloperStore.Sales.Application.DTOs.Outputs;
using DeveloperStore.Sales.Application.Interfaces.Messaging;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Infrastructure.UnitOfWork;

namespace DeveloperStore.Sales.Application.Services
{
    public class SaleAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleEventPublisher _eventPublisher;

        public SaleAppService(IUnitOfWork unitOfWork, ISaleEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> CreateAsync(SaleInputDto dto)
        {
            var sale = new Sale(
                dto.SaleNumber,
                dto.SaleDate,
                dto.CustomerId,
                dto.CustomerName,
                dto.BranchId,
                dto.BranchName
            );

            foreach (var item in dto.Items)
            {
                sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
            }

            var repository = _unitOfWork.Repository<Sale>();
            await repository.AddAsync(sale);
            await _unitOfWork.CommitAsync();

            await _eventPublisher.PublishSaleCreatedAsync(sale.Id);

            return sale.Id;
        }

        public async Task<SaleOutputDto?> GetByIdAsync(Guid id)
        {
            var repository = _unitOfWork.Repository<Sale>();
            var sale = await repository.GetAsync(x => x.Id == id);

            if (sale is null) return null;

            return new SaleOutputDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                Items = sale.Items.Select(i => new SaleItemOutputDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task CancelAsync(Guid saleId)
        {
            var repository = _unitOfWork.Repository<Sale>();
            var sale = await repository.GetAsync(x => x.Id == saleId);

            if (sale is null)
                throw new InvalidOperationException("Sale not found.");

            sale.Cancel();
            repository.Update(sale);
            await _unitOfWork.CommitAsync();

            await _eventPublisher.PublishSaleCancelledAsync(sale.Id);
        }

        public async Task<PagedResult<SaleOutputDto>> ListPagedAsync(int page, int pageSize)
        {
            var repository = _unitOfWork.Repository<Sale>();

            var result = await repository.GetPagedAsync(s => true, page, pageSize);

            var saleDtos = result.Items.Select(sale => new SaleOutputDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                BranchId = sale.BranchId,
                BranchName = sale.BranchName,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                Items = sale.Items.Select(i => new SaleItemOutputDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });

            return new PagedResult<SaleOutputDto>
            {
                Items = saleDtos,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

    }
}
