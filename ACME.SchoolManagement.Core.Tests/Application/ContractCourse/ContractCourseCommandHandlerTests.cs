using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using AutoMapper;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.ContractCourse
{
    public class ContractCourseCommandHandlerTests
    {
        private readonly Mock<ILoggerService> _loggerMock;
        private readonly Mock<IEnrollmentService> _enrollmentServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPaymentGateway> _paymentGatewayMock;
        private readonly ContractCourseCommandHandler _handler;

        public ContractCourseCommandHandlerTests()
        {
            _loggerMock = new Mock<ILoggerService>();
            _enrollmentServiceMock = new Mock<IEnrollmentService>();
            _mapperMock = new Mock<IMapper>();
            _paymentGatewayMock = new Mock<IPaymentGateway>();
            _handler = new ContractCourseCommandHandler();
        }

        [Fact]
        public async Task Handle_InvalidDataConsistency_ThrowsInvalidOperationException()
        {
            var command = new ContractCourseCommand { StudentID = Guid.NewGuid(), CourseID = Guid.NewGuid() };

            _paymentGatewayMock.Setup(pg => pg.ProcessPaymentAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<decimal>()))
                               .ReturnsAsync(true);
            _enrollmentServiceMock.Setup(es => es.CourseExistsAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(false);

            var serviceFactory = new Mock<ServiceFactory>().Object;

            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, serviceFactory, CancellationToken.None));
        }


    }
}
