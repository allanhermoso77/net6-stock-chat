using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Chat.Api.Configurations;
using Stock.Chat.Application.AutoMapper;
using Stock.Chat.Infrastructure.Data.Context;
using Stock.Chat.CrossCutting.Models;
using Stock.Chat.Infrastructure.Security;
using Stock.Chat.Infrastructure.InversionOfControl;

namespace Stock.Chat.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddDbContext<StockChatContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StockChatConnection")));
            services.AddIdentitySetup(Configuration);
            AutoMapperConfig.RegisterMappings();
            services.AddSwagger();
            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
            services.AddMvc();
            services.AddLogging();
            services.AddHttpClient("StockChat", cfg => { cfg.Timeout = TimeSpan.FromSeconds(60); });
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(Startup));
            services.Configure<RabbitMqOptions>(options => Configuration.GetSection("RabbitMqConfig").Bind(options));
            services.AddMassTransit(Configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>());

            services.RegisterServices();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(15);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.AddMiddlewares();
            app.UseSwaggerSetup();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMigration<StockChatContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
                endpoints.MapHub<Application.SignalR.MessageChatHub>("/chatHub", options =>
                {
                    options.TransportMaxBufferSize = 36000;
                    options.ApplicationMaxBufferSize = 36000;
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                });
            });
        }
    }
}
