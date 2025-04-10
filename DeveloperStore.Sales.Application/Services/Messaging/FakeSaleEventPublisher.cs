using DeveloperStore.Sales.Application.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Sales.Application.Services.Messaging
{
    public class FakeSaleEventPublisher : ISaleEventPublisher
    {
        private readonly ILogger<FakeSaleEventPublisher> _logger;

        public FakeSaleEventPublisher(ILogger<FakeSaleEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishSaleCreatedAsync(Guid saleId)
        {
            _logger.LogInformation($"[Queue Simulated]-[Event] SaleCreated: {saleId}");
            return Task.CompletedTask;
        }

        public Task PublishSaleCancelledAsync(Guid saleId)
        {
            _logger.LogInformation($"[Queue Simulated]-[Event] SaleCancelled: {saleId}");
            return Task.CompletedTask;
        }
    }
}
