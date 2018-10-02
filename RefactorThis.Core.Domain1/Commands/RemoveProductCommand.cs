using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.API.Application.Commands
{
    public class RemoveProductCommand : Command
    {
        private Guid id;

        public RemoveProductCommand(Guid id)
        {
            this.id = id;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
