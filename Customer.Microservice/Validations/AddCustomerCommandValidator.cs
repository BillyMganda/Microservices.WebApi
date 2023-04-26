using Customer.Microservice.CQRS;
using FluentValidation;

namespace Customer.Microservice.Validations
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(3).WithMessage("{PropertyName} min length is 3 characters");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(3).WithMessage("{PropertyName} min length is 3 characters");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email address is not valid.")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(5).WithMessage("{PropertyName} min length is 5 characters");

            RuleFor(m => m.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{1,3}[\s.-]?\d{3}[\s.-]?\d{3}[\s.-]?\d{4}$")
            .WithMessage("Invalid phone number format.");

            RuleFor(c => c.City)                
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(3).WithMessage("{PropertyName} min length is 3 characters");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(2).WithMessage("{PropertyName} min length is 2 characters");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} max length is 50 characters")
                .MinimumLength(4).WithMessage("{PropertyName} min length is 4 characters");
        }
    }
}
