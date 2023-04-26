using FluentValidation;
using Order.Microservice.CQRS;

namespace Order.Microservice.Validations
{
    public class PostOrderCommandValidator : AbstractValidator<PostOrderCommand>
    {
        public PostOrderCommandValidator()
        {
            RuleFor(x => x.ProductIds)
            .Must(id => Guid.TryParse(id.ToString(), out var _))
            .WithMessage("Invalid GUID format.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(o => o.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
