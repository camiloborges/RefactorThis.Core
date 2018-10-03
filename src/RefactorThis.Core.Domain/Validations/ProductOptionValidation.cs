using System;
using FluentValidation;
using RefactorThis.Core.Domain.Commands;

namespace RefactorThis.Core.Domain.Validations
{
    public abstract class ProductOptionValidation<T> : AbstractValidator<T> where T : ProductOptionCommand
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