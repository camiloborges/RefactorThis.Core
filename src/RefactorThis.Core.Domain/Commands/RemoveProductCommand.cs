using RefactorThis.Core.Domain.Validations;
using System;

namespace RefactorThis.Core.Domain.Commands
{
    public class RemoveProductCommand : ProductCommand
    {
        public RemoveProductCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
