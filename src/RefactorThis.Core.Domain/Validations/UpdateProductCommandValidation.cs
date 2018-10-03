
using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class UpdateProductCommandValidation : ProductValidation<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidatePrice();
        }
    }
}