

using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class RemoveProductOptionCommandValidation : ProductOptionValidation<UpdateProductOptionCommand>
    {
        public RemoveProductOptionCommandValidation()
        {
            ValidateId();
            ValidateProductId();
        }
    }
}