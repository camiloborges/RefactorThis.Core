using MediatR;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Core.Events;
using System.Threading.Tasks;

namespace RefactorThis.Core.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseMediatorEvent<T>(T @event) where T : DomainEvent
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}