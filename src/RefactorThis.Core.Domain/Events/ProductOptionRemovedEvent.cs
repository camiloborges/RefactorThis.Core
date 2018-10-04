using RefactorThis.Core.Domain.Core.Events;
using System;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductOptionRemovedEvent : DomainEvent
    {
        public ProductOptionRemovedEvent(Guid productId, Guid id)
        {
            Id = id;
            ProductId = productId;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}
