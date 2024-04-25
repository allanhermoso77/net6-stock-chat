using System;

namespace Stock.Chat.CrossCutting.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
