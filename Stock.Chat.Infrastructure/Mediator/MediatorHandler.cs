using MediatR;
using Stock.Chat.CrossCutting.Commands;
using Stock.Chat.CrossCutting.Handler;

namespace Stock.Chat.Infrastructure.MessageQueue
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator) => _mediator = mediator;

        public Task RaiseEvent<T>(T @event) where T : class => _mediator.Publish(@event);
        public async Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command) => await _mediator.Send(command);
    }
}
