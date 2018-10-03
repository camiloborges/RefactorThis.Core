

using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class CreateProductCommandValidation : ProductValidation<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            ValidateName();
            ValidatePrice();
        }
    }
}