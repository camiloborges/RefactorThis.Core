using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Core.Events;
using System.Threading.Tasks;


namespace RefactorThis.Core.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
