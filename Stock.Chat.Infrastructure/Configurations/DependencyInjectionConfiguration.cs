using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Stock.Chat.Application.Services;
using Stock.Chat.Domain.CommandHandlers;
using Stock.Chat.Domain.Commands;
using Stock.Chat.Domain.Commands.Message;
using Stock.Chat.Domain.Interfaces;
using Stock.Chat.Domain.Interfaces.Messaging;
using Stock.Chat.Domain.Interfaces.Services;
using Stock.Chat.Infrastructure.Data;
using Stock.Chat.Infrastructure.Data.Context;
using Stock.Chat.Infrastructure.Data.Repositories;
using Stock.Chat.Infrastructure.MessageQueue;
using Stock.Chat.CrossCutting.Handler;
using Stock.Chat.CrossCutting.Notifications;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Infrastructure.InversionOfControl
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterData();
            services.RegisterHandlers();
            services.RegisterApplicationServices();
        }

        private static void RegisterData(this IServiceCollection services)
        {
            services.AddDbContext<StockChatContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<UserAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<MessageAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUserCommand, TokenJWT>, IdentityHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IQueueMessageService, QueueMessageService>();
        }
    }
}
