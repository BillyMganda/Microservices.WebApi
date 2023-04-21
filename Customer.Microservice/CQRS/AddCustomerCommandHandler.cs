﻿using Customer.Microservice.Models;
using Customer.Microservice.Services;
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
            var customerEntity = new CustomerEntity
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode
            };

            await _customerRepository.CreateAsync(customerEntity);

            return customerEntity.Id;
        }
    }
}