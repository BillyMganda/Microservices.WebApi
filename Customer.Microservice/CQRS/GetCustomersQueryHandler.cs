using Customer.Microservice.DTOs;
using Customer.Microservice.Services;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<GetCustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<GetCustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            var result = customers.Select(c => new GetCustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                City = c.City,
                State = c.State,
                ZipCode = c.ZipCode,
            }).ToList();

            return result;
        }
    }
}
