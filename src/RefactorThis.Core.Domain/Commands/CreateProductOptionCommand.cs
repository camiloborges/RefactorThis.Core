using RefactorThis.Core.Domain.Validations;
using System;

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