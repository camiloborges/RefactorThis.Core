

using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class RemoveProductOptionCommandValidation : ProductOptionValidation<RemoveProductOptionCommand>
    {
        public RemoveProductOptionCommandValidation()
        {
            ValidateId();
            ValidateProductId();
        }
    }
}