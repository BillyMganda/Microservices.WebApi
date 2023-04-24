using Customer.Microservice.DTOs;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerDto>
    {
        public Guid Id { get; set; }
    }
}
