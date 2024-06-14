using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Core.Application.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse;
using AutoMapper;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.RegisterCourse
{
    public class RegisterCourseCommandHandlerTests
    {
        private readonly Mock<ICourseService> _courseServiceMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ServiceFactory _serviceFactory;
        private readonly RegisterCourseCommandHandler _handler;

        public RegisterCourseCommandHandlerTests()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _mapperMock = new Mock<IMapper>();

            // Setup ServiceFactory to return mocks
            _serviceFactory = new ServiceFactory(type =>
            {
                if (type == typeof(ICourseService)) return _courseServiceMock.Object;
                if (type == typeof(ILoggerService)) return _loggerServiceMock.Object;
                if (type == typeof(IMapper)) return _mapperMock.Object;
                throw new ArgumentException("Unexpected type");
            });

            _handler = new RegisterCourseCommandHandler();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnCourseId()
        {
            // Arrange
            var command = new RegisterCourseCommand { Name = "Course 101", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), RegistrationFee = 100 };
            var courseId = Guid.NewGuid().ToString();
            _courseServiceMock.Setup(s => s.RegisterCourse(It.IsAny<Course>())).ReturnsAsync(courseId);

            // Act
            var result = await _handler.Handle(command, _serviceFactory, CancellationToken.None);

            // Assert
            Assert.Equal(courseId, result);
            _courseServiceMock.Verify(s => s.RegisterCourse(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ServiceException_ShouldThrowException()
        {
            // Arrange
            var command = new RegisterCourseCommand { Name = "Course 101", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), RegistrationFee = 100 };
            _courseServiceMock.Setup(s => s.RegisterCourse(It.IsAny<Course>())).ThrowsAsync(new Exception("Service error"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, _serviceFactory, CancellationToken.None));
            Assert.Equal("Service error", ex.Message);
        }
    }
}
