using Customer.Microservice.Exceptions;
using Customer.Microservice.Models;
using Customer.Microservice.Services;
using Customer.Microservice.Validations;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddCustomerCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var customerEntity = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                DateCreated = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _customerRepository.CreateAsync(customerEntity);

            return customerEntity.Id;
        }
    }
}
