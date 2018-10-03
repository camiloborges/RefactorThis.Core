using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain.Commands
{
    public abstract class ProductOptionCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

    }
}