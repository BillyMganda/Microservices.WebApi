using FluentValidation;
using Product.Microservice.CQRS;

namespace Product.Microservice.Validations
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
            .Must(id => Guid.TryParse(id.ToString(), out var _))
            .WithMessage("Invalid GUID format.");

            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
               .MinimumLength(3).WithMessage("{PropertyName} min length is 3 characters");

            RuleFor(c => c.Description)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} max length is 500 characters")
               .MinimumLength(10).WithMessage("{PropertyName} min length is 10 characters");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} should be greater that 0");
        }
    }
}
