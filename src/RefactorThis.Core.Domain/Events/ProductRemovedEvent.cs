using System;
using RefactorThis.Core.Domain.Core.Events;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductRemovedEvent : Event
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}