using Customer.Microservice.Controllers;
using Customer.Microservice.CQRS;
using Customer.Microservice.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.Microservice
{
    public class customersControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly customersController _customersController;
        public customersControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _customersController = new customersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_WithListOfGetCustomerDto()
        {
            // Arrange
            var expectedCustomers = new List<GetCustomerDto>
            {
                new GetCustomerDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "+254 987 654",
                    City = "Nairobi",
                    State = "Nairobi City",
                    ZipCode = "00100",
                    LastModifiedDate = DateTime.Now.AddDays(-1)
                },
                new GetCustomerDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@example.com",
                    PhoneNumber = "+254 123 456",
                    City = "Mombasa",
                    State = "Mombasa City",
                    ZipCode = "00500",
                    LastModifiedDate = DateTime.Now.AddDays(-3)
                }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCustomers);

            // Act
            var result = await _customersController.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualCustomers = Assert.IsAssignableFrom<List<GetCustomerDto>>(okObjectResult.Value);
            Assert.Equal(expectedCustomers.Count, actualCustomers.Count);
            for (int i = 0; i < expectedCustomers.Count; i++)
            {
                Assert.Equal(expectedCustomers[i].Id, actualCustomers[i].Id);
                Assert.Equal(expectedCustomers[i].FirstName, actualCustomers[i].FirstName);
                Assert.Equal(expectedCustomers[i].LastName, actualCustomers[i].LastName);
                Assert.Equal(expectedCustomers[i].Email, actualCustomers[i].Email);
                Assert.Equal(expectedCustomers[i].PhoneNumber, actualCustomers[i].PhoneNumber);
                Assert.Equal(expectedCustomers[i].City, actualCustomers[i].City);
                Assert.Equal(expectedCustomers[i].State, actualCustomers[i].State);
                Assert.Equal(expectedCustomers[i].ZipCode, actualCustomers[i].ZipCode);
                Assert.Equal(expectedCustomers[i].LastModifiedDate, actualCustomers[i].LastModifiedDate);
            }
        }
    }
}