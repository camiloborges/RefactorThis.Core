using RefactorThis.Core.Domain.Core.Commands;
using System;

namespace RefactorThis.Core.Domain.Commands
{
    public class ProductOptionDTO : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
