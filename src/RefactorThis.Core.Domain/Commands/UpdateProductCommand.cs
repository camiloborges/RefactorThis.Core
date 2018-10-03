using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
  
        public UpdateProductCommand(string name, string description, decimal price, decimal deliveryPrice, IList<ProductOptionDTO> options)
        {
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
            Options = options;

        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
