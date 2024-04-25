using AutoMapper;
using Stock.Chat.Domain.Commands;
using Stock.Chat.Domain.Entities;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Application.AutoMapper.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserAddCommand, User>();
            CreateMap<User, UserDto>();
        }
    }
}
