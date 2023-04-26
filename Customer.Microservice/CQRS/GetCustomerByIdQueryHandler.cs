using Customer.Microservice.DTOs;
using Customer.Microservice.Exceptions;
using Customer.Microservice.Services;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new NotFoundException($"{customer!.FirstName}", $"with id {request.Id} not found.");
            }

            var result = new GetCustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,                
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode,
                LastModifiedDate = customer.LastModifiedDate,
            };

            return result;
        }
    }
}
