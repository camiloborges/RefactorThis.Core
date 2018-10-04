using MediatR;
using RefactorThis.Core.Domain.Core.Bus;
using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Core.Notifications;
using RefactorThis.Core.Domain.Interfaces;

namespace RefactorThis.Core.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseMediatorEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _bus.RaiseMediatorEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
