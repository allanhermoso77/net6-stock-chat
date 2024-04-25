using MediatR;
using System;

namespace Stock.Chat.CrossCutting.Event
{
    public abstract class Event : IRequest<bool>, INotification
    {
        public DateTime DateAndTime { get; private set; }
        protected Event()
        {
            DateAndTime = DateTime.Now;
        }
        
    }
}
