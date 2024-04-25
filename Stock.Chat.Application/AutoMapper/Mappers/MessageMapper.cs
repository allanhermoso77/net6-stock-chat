using AutoMapper;
using Stock.Chat.Domain.Commands.Message;
using Stock.Chat.Domain.Entities;
using System;

namespace Stock.Chat.Application.AutoMapper.Mappers
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<MessageAddCommand, Messages>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
