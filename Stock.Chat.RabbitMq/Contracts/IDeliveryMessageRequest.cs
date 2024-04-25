using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.RabbitMq.Contracts
{
    public interface IDeliveryMessageRequest
    {
        Task<ApiOkReturn> DeliveryMessageAsync(MessageDto message);
    }
}
