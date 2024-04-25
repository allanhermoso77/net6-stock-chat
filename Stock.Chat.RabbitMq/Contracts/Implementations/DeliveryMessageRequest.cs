using Polly;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.RabbitMq.Contracts.Implementations
{
    public class DeliveryMessageRequest : IDeliveryMessageRequest
    {
        private readonly ILogger<DeliveryMessageRequest> _logger;
        private readonly IChatService _stockChatService;

        public DeliveryMessageRequest(ILogger<DeliveryMessageRequest> logger, IChatService stockChatService)
        {
            _logger = logger;
            _stockChatService = stockChatService;
        }

        public async Task<ApiOkReturn> DeliveryMessageAsync(MessageDto message) =>
            await Policy.Handle<Exception>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(2),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(exception.Message + " - retrycount: " + retryCount);
                    })
                .ExecuteAsync(() => _stockChatService.CreateApi().DeliveryMessage(message));
    }
}
