using MediatR;
using System;

namespace RefactorThis.Core.Domain.Core.Events
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
