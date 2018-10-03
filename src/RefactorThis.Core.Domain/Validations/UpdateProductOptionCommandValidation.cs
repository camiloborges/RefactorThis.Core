

using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class UpdateProductOptionCommandValidation : ProductOptionValidation<UpdateProductOptionCommand>
    {
        public UpdateProductOptionCommandValidation()
        {
            ValidateName();
            ValidateId();
            ValidateProductId();
        }
    }
}