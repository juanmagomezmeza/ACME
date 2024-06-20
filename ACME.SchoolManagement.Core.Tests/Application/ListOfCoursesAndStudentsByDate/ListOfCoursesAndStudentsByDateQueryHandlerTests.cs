using ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using ACME.SchoolManagement.Core.Domain.Models;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IEnrollmentService> _mockEnrollmentService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<ListOfCoursesAndStudentsByDateQuery>> _mockValidator;
        private readonly ListOfCoursesAndStudentsByDateQueryHandler _handler;
        private readonly Mock<IValidationLogger> _mockValidationLogger;

        public ListOfCoursesAndStudentsByDateQueryHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockEnrollmentService = new Mock<IEnrollmentService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<ListOfCoursesAndStudentsByDateQuery>>();
            _mockValidationLogger = new Mock<IValidationLogger>();

            _handler = new ListOfCoursesAndStudentsByDateQueryHandler(
                _mockLogger.Object,
                _mockEnrollmentService.Object,
                _mockMapper.Object,
                _mockValidator.Object,
                _mockValidationLogger.Object);
        }

        [Fact]
        public async Task HandleRequest_Should_Return_List_Of_EnrollmentModels_When_Successful()
        {
            // Arrange
            var query = new ListOfCoursesAndStudentsByDateQuery
            {
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow
            };

            var enrollments = new List<Enrollment> { new Enrollment() };
            var enrollmentModels = new List<EnrollmentModel> { new EnrollmentModel() };

            _mockValidator.Setup(v => v.Validate(It.IsAny<ListOfCoursesAndStudentsByDateQuery>()))
                .Returns(new ValidationResult());

            _mockEnrollmentService.Setup(es => es.ListOfCoursesAndStudentsByDate(query.StartDate, query.EndDate))
                                  .ReturnsAsync(enrollments);
            _mockMapper.Setup(m => m.Map<EnrollmentModel>(It.IsAny<Enrollment>()))
                       .Returns(enrollmentModels.First());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(enrollmentModels);
        }

        [Fact]
        public async Task HandleRequest_Should_Throw_RequestValidationException_When_Validation_FailsAsync()
        {
            // Arrange
            var query = new ListOfCoursesAndStudentsByDateQuery
            {
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow
            };

            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("StartDate", "StartDate is required."),
            new ValidationFailure("EndDate", "EndDate is required.")
        };

            _mockValidator.Setup(v => v.Validate(It.IsAny<ListOfCoursesAndStudentsByDateQuery>()))
                .Returns(new ValidationResult(validationFailures));

            _mockValidationLogger.Setup(l => l.LogValidationErrors(It.IsAny<ListOfCoursesAndStudentsByDateQuery>(), validationFailures))
                .Returns("Validation failed");

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<RequestValidationException>().WithMessage("Validation failed");
        }

        [Fact]
        public async Task ListOfCoursesAndStudentsByDate_Should_Return_List_Of_EnrollmentModels()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow;

            var enrollments = new List<Enrollment> { new Enrollment() };
            var enrollmentModels = new List<EnrollmentModel> { new EnrollmentModel() };

            _mockEnrollmentService.Setup(es => es.ListOfCoursesAndStudentsByDate(startDate, endDate))
                                  .ReturnsAsync(enrollments);
            _mockMapper.Setup(m => m.Map<EnrollmentModel>(It.IsAny<Enrollment>()))
                       .Returns(enrollmentModels.First());

            // Act
            var result = await _handler.ListOfCoursesAndStudentsByDate(startDate, endDate);

            // Assert
            result.Should().BeEquivalentTo(enrollmentModels);
        }
    }

}
