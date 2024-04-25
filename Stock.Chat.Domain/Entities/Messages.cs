using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Domain.Entities
{
    public class Messages : EntityBase
    {
        public string Message { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Consumer { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
