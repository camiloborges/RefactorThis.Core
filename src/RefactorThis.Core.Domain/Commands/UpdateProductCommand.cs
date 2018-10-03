using RefactorThis.Core.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.API.Application.Commands
{
    public class UpdateProductCommand : Command
    {
        private string name;
        private string description;
        private decimal price;
        private decimal deliveryPrice;

        public UpdateProductCommand(string name, string description, decimal price, decimal deliveryPrice)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.deliveryPrice = deliveryPrice;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
