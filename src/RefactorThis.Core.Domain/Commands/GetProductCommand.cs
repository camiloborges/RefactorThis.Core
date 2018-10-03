using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Commands
{
    public class GetProductCommand : ProductCommand
    {
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
