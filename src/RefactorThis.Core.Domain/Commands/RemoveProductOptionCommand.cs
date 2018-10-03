using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Commands
{
    public class RemoveProductOptionCommand : ProductOptionCommand
    {
        public RemoveProductOptionCommand(Guid productId, Guid id)
        {
            ProductId = productId;
            Id = id;
        }
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
