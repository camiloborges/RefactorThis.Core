using RefactorThis.Core.Domain.Core.Events;
using System;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductUpdatedEvent : DomainEvent
    {
        public ProductUpdatedEvent(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
        public decimal DeliveryPrice { get; private set; }
    }
}
