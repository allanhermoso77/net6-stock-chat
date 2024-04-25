using Stock.Chat.CrossCutting.Commands;
using System.Threading.Tasks;

namespace Stock.Chat.CrossCutting.Handler
{
    public interface IMediatorHandler
    {
        Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}
