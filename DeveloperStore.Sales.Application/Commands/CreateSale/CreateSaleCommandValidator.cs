using FluentValidation;

namespace DeveloperStore.Sales.Application.Commands.CreateSale
{
    public sealed class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .WithMessage("Sale number is required.");

            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer ID is required.");

            RuleFor(x => x.BranchId)
                .NotEqual(Guid.Empty)
                .WithMessage("Branch ID is required.");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Sale items are required.")
                .Must(items => items.Any()).WithMessage("At least one item is required.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateSaleItemDtoValidator());
        }
    }
}
