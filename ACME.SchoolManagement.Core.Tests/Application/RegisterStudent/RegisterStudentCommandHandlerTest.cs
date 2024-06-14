using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Core.Application.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using AutoMapper;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.RegisterStudent
{
    public class RegisterStudentCommandHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ServiceFactory _serviceFactory;
        private readonly RegisterStudentCommandHandler _handler;

        public RegisterStudentCommandHandlerTest()
        {
            _studentServiceMock = new Mock<IStudentService>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _mapperMock = new Mock<IMapper>();

            _serviceFactory = new ServiceFactory(type =>
            {
                if (type == typeof(IStudentService)) return _studentServiceMock.Object;
                if (type == typeof(ILoggerService)) return _loggerServiceMock.Object;
                if (type == typeof(IMapper)) return _mapperMock.Object;
                throw new ArgumentException("Unexpected type");
            });

            _handler = new RegisterStudentCommandHandler();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnStudentId()
        {
            var command = new RegisterStudentCommand { Name = "John Doe", Age = 20 };
            var studentId = Guid.NewGuid().ToString();
            _studentServiceMock.Setup(s => s.RegisterStudent(It.IsAny<Student>())).ReturnsAsync(studentId);

            var result = await _handler.Handle(command, _serviceFactory, CancellationToken.None);

            Assert.Equal(studentId, result);
            _studentServiceMock.Verify(s => s.RegisterStudent(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ServiceException_ShouldThrowException()
        {
            var command = new RegisterStudentCommand { Name = "John Doe", Age = 20 };
            _studentServiceMock.Setup(s => s.RegisterStudent(It.IsAny<Student>())).ThrowsAsync(new Exception("Service error"));

            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, _serviceFactory, CancellationToken.None));
            Assert.Equal("Service error", ex.Message);
        }
    }
}
