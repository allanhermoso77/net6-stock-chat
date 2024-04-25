using Stock.Chat.CrossCutting.Models;
using Stock.Chat.Domain.Entities;

namespace Stock.Chat.Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        User Authenticate(string username, string password);
        TokenJWT GetToken(Guid id, string username);
    }
}
