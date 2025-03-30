using DeveloperStore.Sales.Domain.Entities;

namespace DeveloperStore.Sales.Domain.Services.Interfaces
{
    public interface ISaleDomainService
    {
        void ValidateSale(Sale sale);
        void CancelSale(Sale sale);
    }
}
