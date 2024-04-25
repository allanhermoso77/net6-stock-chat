using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
