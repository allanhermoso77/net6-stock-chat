using Stock.Chat.CrossCutting.Models;
using MassTransit;
using Microsoft.Extensions.Options;
using Stock.Chat.Domain.Interfaces.Messaging;

namespace Stock.Chat.Application.Services
{
    public class QueueMessageService : IQueueMessageService
    {
        private readonly IBusControl _busControl;
        private readonly string queueExchange = "queue:{0}";

        public QueueMessageService(IBusControl busControl, IOptions<RabbitMqOptions> options)
        {
            _busControl = busControl;
            queueExchange = string.Format(queueExchange, options.Value.Queue);
        }
        public async Task SendMessageAsync(MessageDto messageDto)
        {
            try
            {
                _busControl.Start();
                var endpoint = await _busControl.GetSendEndpoint(new Uri(queueExchange));
                await endpoint.Send(messageDto);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _busControl.Stop();
            }
        }
    }
}
