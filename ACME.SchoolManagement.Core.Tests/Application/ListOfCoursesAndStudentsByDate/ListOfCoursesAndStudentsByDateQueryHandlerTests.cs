using ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Models;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IEnrollmentService> _mockEnrollmentService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ListOfCoursesAndStudentsByDateQueryHandler _handler;

        public ListOfCoursesAndStudentsByDateQueryHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockEnrollmentService = new Mock<IEnrollmentService>();
            _mockMapper = new Mock<IMapper>();

            _handler = new ListOfCoursesAndStudentsByDateQueryHandler(
                _mockLogger.Object,
                _mockEnrollmentService.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_List_Of_EnrollmentModels_When_Successful()
        {
            // Arrange
            var query = new ListOfCoursesAndStudentsByDateQuery
            {
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow
            };

            var enrollments = new List<Enrollment> { new Enrollment() };
            var enrollmentModels = new List<EnrollmentModel> { new EnrollmentModel() };

            _mockEnrollmentService.Setup(es => es.ListOfCoursesAndStudentsByDate(query.StartDate, query.EndDate))
                                  .ReturnsAsync(enrollments);
            _mockMapper.Setup(m => m.Map<EnrollmentModel>(It.IsAny<Enrollment>()))
                       .Returns(enrollmentModels.First());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(enrollmentModels);
        }

        //[Fact]
        //public void Handle_Should_Throw_RequestValidationException_When_Validation_Fails()
        //{
        //    // Arrange
        //    var query = new ListOfCoursesAndStudentsByDateQuery
        //    {
        //        StartDate = DateTime.UtcNow.AddDays(-1),
        //        EndDate = DateTime.UtcNow
        //    };

        //    var validationFailures = new List<ValidationFailure>
        //{
        //    new ValidationFailure("StartDate", "StartDate is required."),
        //    new ValidationFailure("EndDate", "EndDate is required.")
        //};

        //    _mockLogger.Setup(l => l.LogValidationErrors(query, It.IsAny<IEnumerable<ValidationFailure>>()))
        //               .Returns("Validation failed");

        //    var validatorMock = new Mock<IValidator<ListOfCoursesAndStudentsByDateQuery>>();
        //    validatorMock.Setup(v => v.Validate(query))
        //                 .Returns(new ValidationResult(validationFailures));

        //    // Injecting the mocked validator into the handler
        //    var handlerWithMockedValidator = new ListOfCoursesAndStudentsByDateQueryHandler(
        //        _mockLogger.Object,
        //        _mockEnrollmentService.Object,
        //        _mockMapper.Object)
        //    {
        //        Validator = validatorMock.Object
        //    };

        //    // Act
        //    Func<Task> act = async () => await handlerWithMockedValidator.Handle(query, CancellationToken.None);

        //    // Assert
        //    act.Should().Throw<RequestValidationException>()
        //        .WithMessage("Validation failed");
        //}
    }
}
