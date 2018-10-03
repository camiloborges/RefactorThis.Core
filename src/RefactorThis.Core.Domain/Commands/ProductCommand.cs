using RefactorThis.Core.API.Application.Commands;
using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using static RefactorThis.Core.API.Application.Commands.CreateProductCommand;

namespace RefactorThis.Core.Domain.Commands
{
    public abstract class ProductCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public decimal Price { get; protected set; }
        public decimal DeliveryPrice { get; protected set; }
        public IList<ProductOptionDTO> Options { get; protected set; }

    }
}