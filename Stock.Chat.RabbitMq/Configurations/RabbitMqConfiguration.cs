using Stock.Chat.CrossCutting.Models;
using Stock.Chat.RabbitMq.Consumers;
using MassTransit;

namespace Stock.Chat.RabbitMq.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMq(this IServiceCollection services, RabbitMqOptions rabbitMqOptions)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<MessageConsumer>();
                options.AddConsumer<MessageDlqConsumer>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{rabbitMqOptions.Url}"), h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                    
                    cfg.ReceiveEndpoint(rabbitMqOptions.Queue, e =>
                    {
                        e.UseScheduledRedelivery(r => r.Intervals(new TimeSpan[] { TimeSpan.FromSeconds(15), TimeSpan.FromMinutes(1) }));

                        e.DiscardFaultedMessages();

                        e.ConfigureConsumer<MessageConsumer>(context);
                        e.ConfigureConsumer<MessageDlqConsumer>(context);
                    });
                });
            });
        }
    }
}
