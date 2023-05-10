using Customer.Microservice.Controllers;
using Customer.Microservice.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Order.Microservice.Controllers;
using Order.Microservice.CQRS;
using Order.Microservice.DTOs;

namespace Test.Microservice
{
    public class ordersControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ordersController _ordersController;
        public ordersControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _ordersController = new ordersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_WithListOfGetOrderDto()
        {
            // Arrange
            var anyId = Guid.NewGuid();
            var expectedOrders = new List<GetOrderDto>
            {
                new GetOrderDto
                {
                    Id = anyId,
                    CustomerId = anyId,              
                    ProductId = new List<Guid>(),
                    OrderDate = DateTime.Now.AddDays(-1),
                    Price = 10,
                    Quantity = 10
                },
                new GetOrderDto
                {
                    Id = anyId,
                    CustomerId = anyId,
                    ProductId = new List<Guid>(),
                    OrderDate = DateTime.Now.AddDays(-2),
                    Price = 20,
                    Quantity = 20
                }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedOrders);

            // Act
            var result = await _ordersController.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualOrders = Assert.IsAssignableFrom<List<GetOrderDto>>(okObjectResult.Value);

            Assert.Equal(expectedOrders.Count, actualOrders.Count);
            for (int i = 0; i < expectedOrders.Count; i++)
            {
                Assert.Equal(expectedOrders[i].Id, actualOrders[i].Id);
                Assert.Equal(expectedOrders[i].CustomerId, actualOrders[i].CustomerId);
                Assert.Equal(expectedOrders[i].ProductId, actualOrders[i].ProductId);
                Assert.Equal(expectedOrders[i].OrderDate, actualOrders[i].OrderDate);
                Assert.Equal(expectedOrders[i].Price, actualOrders[i].Price);
                Assert.Equal(expectedOrders[i].Quantity, actualOrders[i].Quantity);
            }
        }

        [Fact]
        public async Task GetOrderByOrderId_ReturnsOkObjectResult_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder = new GetOrderDto
            {
                Id = orderId,
                CustomerId = orderId,
                ProductId = new List<Guid>(),
                OrderDate = DateTime.Now.AddDays(-2),
                Price = 20,
                Quantity = 20
            };
            var query = new GetOrderByOrderIdQuery { OrderId = orderId };
            _mediatorMock
                .Setup(m => m.Send(It.Is<GetOrderByOrderIdQuery>(q => q.OrderId == orderId), default))
                .ReturnsAsync(expectedOrder);


            // Act
            var result = await _ordersController.GetOrderByOrderId(orderId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualOrder = Assert.IsType<GetOrderDto>(okObjectResult.Value);
            Assert.Equal(expectedOrder.Id, actualOrder.Id);
            Assert.Equal(expectedOrder.CustomerId, actualOrder.CustomerId);
            Assert.Equal(expectedOrder.ProductId, actualOrder.ProductId);
            Assert.Equal(expectedOrder.OrderDate, actualOrder.OrderDate);
            Assert.Equal(expectedOrder.Price, actualOrder.Price);
            Assert.Equal(expectedOrder.Quantity, actualOrder.Quantity);
        }


        [Fact]
        public async Task GetOrderByOrderId_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var query = new GetOrderByOrderIdQuery { OrderId = orderId };
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync((GetOrderDto)null);
            var controller = new customersController(_mediatorMock.Object);

            // Act
            var result = await _ordersController.GetOrderByOrderId(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}
