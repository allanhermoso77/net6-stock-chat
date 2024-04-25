using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Domain.Interfaces.Messaging
{
    public interface IQueueMessageService
    {
        Task SendMessageAsync(MessageDto messageDto);
    }
}
