using Stock.Chat.Domain.Entities;
using Stock.Chat.Domain.Interfaces;
using Stock.Chat.Infrastructure.Data.Context;

namespace Stock.Chat.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(StockChatContext stockChatContext) : base(stockChatContext) {}

        public void Add(Messages messages) => Db.Messages.Add(messages);
        public async Task AddAsync(Messages messages, CancellationToken cancellationToken) => await Db.Messages.AddAsync(messages, cancellationToken);
        public IEnumerable<Messages> GetMessages() => Db.Messages.OrderByDescending(x => x.Date).Take(50);
    }
}
