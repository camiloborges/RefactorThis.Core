using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
#pragma warning disable CA1710 // Identifiers should have correct suffix

    public class CreateProductOptionCommandValidation : ProductOptionValidation<CreateProductOptionCommand>
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        public CreateProductOptionCommandValidation()
        {
            ValidateName();
            ValidateProductId();
        }
    }
}