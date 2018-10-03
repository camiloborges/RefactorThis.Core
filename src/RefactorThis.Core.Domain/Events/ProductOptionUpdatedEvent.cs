using RefactorThis.Core.Domain.Core.Events;
using System;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductOptionUpdatedEvent : DomainEvent
    {
        public ProductOptionUpdatedEvent(Guid id, Guid productId)
        {
            Id = id;
            ProductId = productId;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}