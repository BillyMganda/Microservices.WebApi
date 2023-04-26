using FluentValidation;
using Order.Microservice.CQRS;

namespace Order.Microservice.Validations
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
            .Must(id => Guid.TryParse(id.ToString(), out var _))
            .WithMessage("Invalid GUID format.");
        }
    }
}
