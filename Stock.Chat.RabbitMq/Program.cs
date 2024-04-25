using Stock.Chat.CrossCutting.Handler;
using Stock.Chat.CrossCutting.Models;
using Stock.Chat.Infrastructure.Security;
using Stock.Chat.RabbitMq.Configurations;
using Stock.Chat.RabbitMq.Contracts;
using Stock.Chat.RabbitMq.Contracts.Implementations;

namespace Stock.Chat.MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    Console.WriteLine(hostingContext.HostingEnvironment.EnvironmentName);
                    config
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((c, l) =>
                {
                    l.AddConfiguration(c.Configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    
                    var configuration = hostContext.Configuration;
                    services.AddScoped<LoggingHttpHandler>();
                    services.AddScoped<IChatService, ChatService>();
                    services.AddScoped<IDeliveryMessageRequest, DeliveryMessageRequest>();
                    var rabbitMqOptions = configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>();
                    services.AddRabbitMq(rabbitMqOptions);
                    services.AddIdentitySetup(configuration);
                    services.AddHttpClient("stockchat", c =>
                    {
                        c.Timeout = TimeSpan.FromSeconds(5);
                        c.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
                    });
                });

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Environment.Exit(1);
        }
    }
}
