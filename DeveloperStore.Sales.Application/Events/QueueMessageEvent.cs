using MediatR;

namespace DeveloperStore.Sales.Application.Events
{
    public class QueueMessageEvent : INotification
    {
        public string Message { get; private set; }
        public string QueueName { get; private set; }
        public DateTime Timestamp { get; private set; }

        public QueueMessageEvent(string message, string queueName)
        {
            Message = message;
            QueueName = queueName;
            Timestamp = DateTime.UtcNow;
        }
    }
} 