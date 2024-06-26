﻿using MassTransit;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Api.Configurations
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransit(this IServiceCollection services, RabbitMqOptions options)
        {

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(options.Url), h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });

            services.AddSingleton(busControl);
        }
    }
}
