

using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class CreateProductOptionCommandValidation : ProductOptionValidation<CreateProductOptionCommand>
    {
        public CreateProductOptionCommandValidation()
        {
            ValidateName();
            ValidateProductId();
        }
    }
}