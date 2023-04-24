using Customer.Microservice.DTOs;
using MediatR;

namespace Customer.Microservice.CQRS
{
    public class GetCustomersQuery : IRequest<List<GetCustomerDto>>
    {
    }
}
