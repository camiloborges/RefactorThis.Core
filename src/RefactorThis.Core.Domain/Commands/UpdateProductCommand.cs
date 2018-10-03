using RefactorThis.Core.Domain.Validations;
using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        public UpdateProductCommand(Guid id, string name, string description, decimal price, decimal deliveryPrice, IList<ProductOptionDTO> options)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
            Options = options;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}