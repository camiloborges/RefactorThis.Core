using FluentValidation;
using RefactorThis.Core.Domain.Commands;
using System;

namespace RefactorThis.Core.Domain.Validations
{
#pragma warning disable CA1710 // Identifiers should have correct suffix

    public abstract class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidatePrice()
        {
            RuleFor(c => c.Price)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Price must be more than 0");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
