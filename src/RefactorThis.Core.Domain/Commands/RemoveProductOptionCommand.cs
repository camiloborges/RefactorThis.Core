using RefactorThis.Core.Domain.Validations;
using System;

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
            ValidationResult = new RemoveProductOptionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
