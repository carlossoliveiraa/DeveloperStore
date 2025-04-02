namespace DeveloperStore.Sales.Application.Interfaces.Messaging
{
    public interface ISaleEventPublisher
    {
        Task PublishSaleCreatedAsync(Guid saleId);
        Task PublishSaleCancelledAsync(Guid saleId);
    }
}
