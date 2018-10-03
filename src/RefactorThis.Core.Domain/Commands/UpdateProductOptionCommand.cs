using RefactorThis.Core.Domain.Core.Commands;
using RefactorThis.Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Core.Domain.Commands
{
    public class UpdateProductOptionCommand : ProductOptionCommand
    {
        public UpdateProductOptionCommand(Guid productId, Guid id)
        {
            ProductId = productId;
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateProductOptionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
