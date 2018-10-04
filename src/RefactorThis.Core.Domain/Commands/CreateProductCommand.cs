using RefactorThis.Core.Domain.Validations;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain.Commands
{
    public partial class CreateProductCommand : ProductCommand
    {
        public CreateProductCommand(string name, string description, decimal price, decimal deliveryPrice, IList<ProductOptionDTO> options)
        {
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
            Options = options;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
