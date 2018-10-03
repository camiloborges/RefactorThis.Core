using RefactorThis.Core.Domain.Core.Events;
using System;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductRemovedEvent : DomainEvent
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}