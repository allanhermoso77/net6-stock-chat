using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Stock.Chat.Api.Configurations
{
    public static class MigrationConfiguration
    {
        public static void AddMigration<T>(this IApplicationBuilder app) where T : Stock.Chat.Infrastructure.Data.Context.StockChatContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<T>();
            dbContext?.Database.Migrate();
        }
    }
}