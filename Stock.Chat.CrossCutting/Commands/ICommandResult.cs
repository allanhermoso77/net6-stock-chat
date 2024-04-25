using MediatR;

namespace Stock.Chat.CrossCutting.Commands
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool IsValid();
    }
}
