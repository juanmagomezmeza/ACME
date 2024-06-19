using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.ContractCourse
{
    public class ContractCourseCommandHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IEnrollmentService> _mockEnrollmentService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IPaymentGateway> _mockPaymentGateway;
        private readonly ContractCourseCommandHandler _handler;

        public ContractCourseCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockEnrollmentService = new Mock<IEnrollmentService>();
            _mockMapper = new Mock<IMapper>();
            _mockPaymentGateway = new Mock<IPaymentGateway>();

            _handler = new ContractCourseCommandHandler(
                _mockLogger.Object,
                _mockEnrollmentService.Object,
                _mockMapper.Object,
                _mockPaymentGateway.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Enrollment_Id_When_Successful()
        {
            // Arrange
            var command = new ContractCourseCommand
            {
                StudentID = Guid.NewGuid(),
                CourseID = Guid.NewGuid()
            };

            var enrollment = new Enrollment();

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, 234234))
                               .ReturnsAsync(true);
            _mockEnrollmentService.Setup(es => es.CourseExistsAsync(command.CourseID))
                                  .ReturnsAsync(true);
            _mockEnrollmentService.Setup(es => es.StudentExistsAsync(command.StudentID))
                                  .ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<Enrollment>(command))
                       .Returns(enrollment);
            _mockEnrollmentService.Setup(es => es.Enroll(enrollment))
                                  .ReturnsAsync("enrollment1");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be("enrollment1");
        }

        [Fact]
        public async Task Handle_Should_Throw_UnauthorizedAccessException_When_Payment_Fails()
        {
            // Arrange
            var command = new ContractCourseCommand
            {
                StudentID = Guid.NewGuid(),
                CourseID = Guid.NewGuid()
            };

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, 234234))
                               .ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                      .WithMessage("Paid error");
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidDataException_When_Data_Is_Invalid()
        {
            // Arrange
            var command = new ContractCourseCommand
            {
                StudentID = Guid.NewGuid(),
                CourseID = Guid.NewGuid()
            };

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, 234234))
                               .ReturnsAsync(true);
            _mockEnrollmentService.Setup(es => es.CourseExistsAsync(command.CourseID))
                                  .ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidDataException>()
                      .WithMessage("Invalid data");
        }

        //[Fact]
        //public void Handle_Should_Throw_RequestValidationException_When_Validation_Fails()
        //{
        //    // Arrange
        //    var command = new ContractCourseCommand(); // Invalid command

        //    var validationFailures = new List<ValidationFailure>
        //{
        //    new ValidationFailure("StudentID", "StudentID is required."),
        //    new ValidationFailure("CourseID", "CourseID is required.")
        //};

        //    _mockLogger.Setup(l => l.LogValidationErrors(command, It.IsAny<IEnumerable<ValidationFailure>>()))
        //               .Returns("Validation failed");

        //    // Act
        //    Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    act.Should().Throw<RequestValidationException>()
        //        .WithMessage("Validation failed");
        //}
    }
}
