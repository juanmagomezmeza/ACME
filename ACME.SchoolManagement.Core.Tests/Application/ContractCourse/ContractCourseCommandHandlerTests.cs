using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
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
        private readonly Mock<IValidator<ContractCourseCommand>> _mockValidator;
        private readonly ContractCourseCommandHandler _handler;
        private readonly Mock<IValidationLogger> _mockValidationLogger;

        public ContractCourseCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockEnrollmentService = new Mock<IEnrollmentService>();
            _mockMapper = new Mock<IMapper>();
            _mockPaymentGateway = new Mock<IPaymentGateway>();
            _mockValidator = new Mock<IValidator<ContractCourseCommand>>();
            _mockValidationLogger = new Mock<IValidationLogger>();

            _handler = new ContractCourseCommandHandler(
                _mockLogger.Object,
                _mockEnrollmentService.Object,
                _mockMapper.Object,
                _mockPaymentGateway.Object,
                _mockValidator.Object,
                _mockValidationLogger.Object);
        }

        [Fact]
        public async Task HandleRequest_Should_Return_EnrollmentId_When_Successful()
        {
            var command = new ContractCourseCommand { StudentID = Guid.NewGuid(), CourseID = Guid.NewGuid() };
            var enrollmentId = "EnrollmentId";

            _mockValidator.Setup(v => v.Validate(It.IsAny<ContractCourseCommand>()))
                .Returns(new ValidationResult());

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, It.IsAny<decimal>()))
                .ReturnsAsync(true);

            _mockEnrollmentService.Setup(es => es.CourseExistsAsync(command.CourseID))
                .ReturnsAsync(true);

            _mockEnrollmentService.Setup(es => es.StudentExistsAsync(command.StudentID))
                .ReturnsAsync(true);

            _mockMapper.Setup(m => m.Map<Enrollment>(command))
                .Returns(new Enrollment());

            _mockEnrollmentService.Setup(es => es.Enroll(It.IsAny<Enrollment>()))
                .ReturnsAsync(enrollmentId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(enrollmentId);
        }

        [Fact]
        public async Task HandleRequest_Should_Throw_UnauthorizedAccessException_When_Payment_Fails()
        {
            var command = new ContractCourseCommand { StudentID = Guid.NewGuid(), CourseID = Guid.NewGuid() };

            _mockValidator.Setup(v => v.Validate(It.IsAny<ContractCourseCommand>()))
                .Returns(new ValidationResult());

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, It.IsAny<decimal>()))
                .ReturnsAsync(false);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Paid error");
        }

        [Fact]
        public async Task HandleRequest_Should_Throw_InvalidDataException_When_Data_Is_Inconsistent()
        {
            var command = new ContractCourseCommand { StudentID = Guid.NewGuid(), CourseID = Guid.NewGuid() };

            _mockValidator.Setup(v => v.Validate(It.IsAny<ContractCourseCommand>()))
                .Returns(new ValidationResult());

            _mockPaymentGateway.Setup(pg => pg.ProcessPaymentAsync(command.StudentID, command.CourseID, It.IsAny<decimal>()))
                .ReturnsAsync(true);

            _mockEnrollmentService.Setup(es => es.CourseExistsAsync(command.CourseID))
                .ReturnsAsync(false);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidDataException>().WithMessage("Invalid data");
        }

        [Fact]
        public void HandleRequest_Should_Throw_RequestValidationException_When_Validation_Fails()
        {
            var command = new ContractCourseCommand { StudentID = Guid.NewGuid(), CourseID = Guid.NewGuid() };

            var validationFailures = new List<ValidationFailure>
        {
            new("StudentID", "StudentID is required."),
            new("CourseID", "CourseID is required.")
        };

            _mockValidator.Setup(v => v.Validate(It.IsAny<ContractCourseCommand>()))
                .Returns(new ValidationResult(validationFailures));

            _mockValidationLogger.Setup(l => l.LogValidationErrors(It.IsAny<ContractCourseCommand>(), validationFailures))
                .Returns("Validation failed");

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<RequestValidationException>().WithMessage("Validation failed");
        }
    }

}
