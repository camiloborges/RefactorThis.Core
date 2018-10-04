using RefactorThis.Core.Domain.Core.Events;
using System;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductOptionCreatedEvent : DomainEvent
    {
        public ProductOptionCreatedEvent(Guid id, Guid productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
