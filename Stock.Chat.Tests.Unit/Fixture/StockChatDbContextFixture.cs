using Stock.Chat.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Chat.Tests.Fixture
{
    public class StockChatDbContextFixure
    {
        protected StockChatContext db;

        protected static DbContextOptions<StockChatContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<StockChatContext>();
            builder.UseInMemoryDatabase("StockDbTest")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected static StockChatContext GetDbInstance() => new(CreateNewContextOptions());
    }
}
