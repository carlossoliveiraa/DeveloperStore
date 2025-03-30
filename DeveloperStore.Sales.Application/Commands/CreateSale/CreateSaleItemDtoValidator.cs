using FluentValidation;

namespace DeveloperStore.Sales.Application.Commands.CreateSale
{
    public sealed class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
    {
        public CreateSaleItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Maximum 20 items allowed per product.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
