using Stock.Chat.Domain.Entities;
using Stock.Chat.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Chat.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void Add(Messages messages);
        Task AddAsync(Messages messages, CancellationToken cancellationToken);
        IEnumerable<Messages> GetMessages();
    }
}
