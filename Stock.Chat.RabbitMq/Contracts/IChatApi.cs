using Refit;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.RabbitMq.Contracts
{
    public interface IChatApi
    {
        [Post("/users/receive")]
        Task<ApiOkReturn> DeliveryMessage([Body] MessageDto transaction);
    }
}
