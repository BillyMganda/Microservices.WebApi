using Customer.Microservice.Controllers;
using MediatR;
using Moq;
using Order.Microservice.Controllers;

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
    }
}
