using ACME.SchoolManagement.Core.Application.Services.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using AutoMapper;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryHandlerTests
    {
        private readonly Mock<IEnrollmentService> _enrollmentServiceMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ServiceFactory _serviceFactory;
        private readonly ListOfCoursesAndStudentsByDateQueryHandler _handler;

        public ListOfCoursesAndStudentsByDateQueryHandlerTests()
        {
            _enrollmentServiceMock = new Mock<IEnrollmentService>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _mapperMock = new Mock<IMapper>();

            // Setup ServiceFactory to return mocks
            _serviceFactory = new ServiceFactory(type =>
            {
                if (type == typeof(IEnrollmentService)) return _enrollmentServiceMock.Object;
                if (type == typeof(ILoggerService)) return _loggerServiceMock.Object;
                if (type == typeof(IMapper)) return _mapperMock.Object;
                throw new ArgumentException("Unexpected type");
            });

            _handler = new ListOfCoursesAndStudentsByDateQueryHandler();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnEnrollmentList()
        {
            // Arrange
            var query = new ListOfCoursesAndStudentsByDateQuery { StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow };
            var enrollmentList = new List<Enrollment>
        {
            new Enrollment
            {
                Student = new Student { StudentID = Guid.NewGuid(), Name = "John Doe", Age = 20 },
                Course = new Course { CourseID = Guid.NewGuid(), Name = "Course 101", StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow, RegistrationFee = 100 }
            }
        };
            _enrollmentServiceMock.Setup(s => s.ListOfCoursesAndStudentsByDate(query.StartDate, query.EndDate)).ReturnsAsync(enrollmentList);

            // Act
            var result = await _handler.Handle(query, _serviceFactory, CancellationToken.None);

            // Assert
            Assert.Single(result);
            _enrollmentServiceMock.Verify(s => s.ListOfCoursesAndStudentsByDate(query.StartDate, query.EndDate), Times.Once);
        }

        [Fact]
        public async Task Handle_ServiceException_ShouldThrowException()
        {
            // Arrange
            var query = new ListOfCoursesAndStudentsByDateQuery { StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow };
            _enrollmentServiceMock.Setup(s => s.ListOfCoursesAndStudentsByDate(query.StartDate, query.EndDate)).ThrowsAsync(new Exception("Service error"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, _serviceFactory, CancellationToken.None));
            Assert.Equal("Service error", ex.Message);
        }
    }
}
