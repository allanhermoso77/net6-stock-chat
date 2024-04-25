using Stock.Chat.Domain.Interfaces;
using Stock.Chat.Infrastructure.Data.Context;

namespace Stock.Chat.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockChatContext _stockChatContext;

        public UnitOfWork(StockChatContext stockChatContext) => _stockChatContext = stockChatContext;

        public bool Commit() => _stockChatContext.SaveChanges() > 0;

        public async Task<bool> CommitAsync(CancellationToken cancellationToken) => 
            await _stockChatContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
