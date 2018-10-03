using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Commands
{
    public class CreateProductOptionCommand : ProductOptionCommand
    {
        public CreateProductOptionCommand(Guid productId, string name, string description)
        {
            Name = name;
            Description = description;
            ProductId = productId;
        }
        public override bool IsValid()
        {
            ValidationResult = new CreateProductOptionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
