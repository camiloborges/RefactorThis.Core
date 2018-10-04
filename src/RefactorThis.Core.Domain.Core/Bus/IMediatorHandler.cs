using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Core.Events;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

#pragma warning disable CA1030 // Use events where appropriate

        Task RaiseMediatorEvent<T>(T @mediatorEvent) where T : DomainEvent;

#pragma warning restore CA1030 // Use events where appropriate
    }
}
