using FluentValidation;
using Product.Microservice.CQRS;

namespace Product.Microservice.Validations
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
            .Must(id => Guid.TryParse(id.ToString(), out var _))
            .WithMessage("Invalid GUID format.");
        }
    }
}
