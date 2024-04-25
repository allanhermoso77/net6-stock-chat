using Stock.Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Domain.Interfaces.Services
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        UserDto GetUser(Guid id);
        List<Messages> GetMessages();
        List<Messages> GetMessages(string username);
    }
}
