using Customer.Microservice.Controllers;
using Customer.Microservice.CQRS;
using Customer.Microservice.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
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


        [Fact]
        public async Task GetCustomerById_WithExistingCustomerId_ReturnsOkObjectResult_WithGetCustomerDto()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedCustomer = new GetCustomerDto
            {
                Id = customerId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "+254 987 654",
                City = "Nairobi",
                State = "Nairobi City",
                ZipCode = "00100",
                LastModifiedDate = DateTime.Now.AddDays(-1)
            };
            _mediatorMock.Setup(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.Id == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _customersController.GetCustomerById(customerId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualCustomer = Assert.IsType<GetCustomerDto>(okObjectResult.Value);
            Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
            Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
            Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
            Assert.Equal(expectedCustomer.Email, actualCustomer.Email);
            Assert.Equal(expectedCustomer.PhoneNumber, actualCustomer.PhoneNumber);
            Assert.Equal(expectedCustomer.City, actualCustomer.City);
            Assert.Equal(expectedCustomer.State, actualCustomer.State);
            Assert.Equal(expectedCustomer.ZipCode, actualCustomer.ZipCode);
            Assert.Equal(expectedCustomer.LastModifiedDate, actualCustomer.LastModifiedDate);
        }

        [Fact]
        public async Task GetCustomerById_WithNonExistingCustomerId_ReturnsNotFoundResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.Id == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetCustomerDto)null);

            // Act
            var result = await _customersController.GetCustomerById(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task AddCustomer_ReturnsOkResultWithCustomerId()
        {
            // Arrange
            var command = new AddCustomerCommand 
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "+254 987 654",
                City = "Nairobi",
                State = "Nairobi City",
                ZipCode = "00100"
            };
            var expectedCustomerId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddCustomerCommand>(), default))
                .ReturnsAsync(expectedCustomerId);

            // Act
            var result = await _customersController.AddCustomer(command);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Guid>>(result);
            var actualCustomerId = Assert.IsType<Guid>(actionResult.Value);
            Assert.Equal(expectedCustomerId, actualCustomerId);
        }


        [Fact]
        public async Task UpdateCustomer_ReturnsOkResult_WhenCustomerIsUpdatedSuccessfully()
        {
            // Arrange
            var command = new UpdateCustomerCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "+254 987 654",
                City = "Nairobi",
                State = "Nairobi City",
                ZipCode = "00100"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), default))
                .ReturnsAsync("Customer updated successfully.");

            // Act
            var result = await _customersController.UpdateCustomer(command);

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

    }
}