using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Sales.Application.Events.Handlers
{
    public class QueueMessageEventHandler : INotificationHandler<QueueMessageEvent>
    {
        private readonly ILogger<QueueMessageEventHandler> _logger;

        public QueueMessageEventHandler(ILogger<QueueMessageEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(QueueMessageEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Queue Simulated] -> Queue: {Queue}, Message: {Message}, Timestamp: {Timestamp}",
                notification.QueueName,
                notification.Message,
                notification.Timestamp);

            return Task.CompletedTask;
        }
    }
} 