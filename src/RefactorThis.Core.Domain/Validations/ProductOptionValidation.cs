using FluentValidation;
using RefactorThis.Core.Domain.Commands;
using System;

namespace RefactorThis.Core.Domain.Validations
{
#pragma warning disable CA1710 // Identifiers should have correct suffix

    public abstract class ProductOptionValidation<T> : AbstractValidator<T> where T : ProductOptionCommand
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateProductId()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty()
                .NotNull()
            .NotEqual(Guid.Empty)
                .WithMessage("Product Id must be provided");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
