
using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public class RemoveProductCommandValidation : ProductValidation<RemoveProductCommand>
    {
        public RemoveProductCommandValidation()
        {
            ValidateId();
        }
    }
}