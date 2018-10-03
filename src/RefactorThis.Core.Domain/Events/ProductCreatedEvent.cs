using System;
using RefactorThis.Core.Domain.Core.Events;

namespace RefactorThis.Core.Domain.Events
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
        public decimal DeliveryPrice { get; private set; }

    }
}