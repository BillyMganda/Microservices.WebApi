using Customer.Microservice.CQRS;
using FluentValidation;

namespace Customer.Microservice.Validations
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
            .Must(id => Guid.TryParse(id.ToString(), out var _))
            .WithMessage("Invalid GUID format.");
        }
    }
}
