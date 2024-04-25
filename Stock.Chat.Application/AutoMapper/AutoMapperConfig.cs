using AutoMapper;
using Stock.Chat.Application.AutoMapper.Mappers;

namespace Stock.Chat.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                cfg.AddProfile(new MessageMapper());
            });
        }
    }
}
