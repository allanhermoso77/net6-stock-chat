using MassTransit;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.RabbitMq.Consumers
{
    public class MessageDlqConsumer : IConsumer<Fault<MessageDto>>
    {
        private readonly ILogger<MessageDlqConsumer> _logger;

        public MessageDlqConsumer(ILogger<MessageDlqConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<MessageDto>> context)
        {
            _logger.LogInformation($"FAULT: Message received: {context.Message}");
            await Task.CompletedTask;
        }
    }
}
