using Customer.Microservice.Services;
using Customer.Microservice.Validations;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCustomerCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
                throw new Exception();

            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with Id {request.Id} not found.");
            }
            await _customerRepository.DeleteAsync(customer);
        }
    }
}
