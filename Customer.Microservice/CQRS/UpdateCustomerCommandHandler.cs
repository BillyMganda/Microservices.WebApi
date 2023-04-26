using Customer.Microservice.Services;
using Customer.Microservice.Validations;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
                throw new Exception();

            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with Id {request.Id} not found.");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.City = request.City;
            customer.State = request.State;
            customer.ZipCode = request.ZipCode;
            customer.LastModifiedDate = DateTime.UtcNow;

            await _customerRepository.UpdateAsync(customer);
            return "Customer updated successfully.";
        }
    }
}
