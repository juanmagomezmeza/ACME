using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.RegisterStudent
{
    public class RegisterStudentCommandHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<RegisterStudentCommand>> _mockValidator;
        private readonly RegisterStudentCommandHandler _handler;
        private readonly Mock<IValidationLogger> _mockValidationLogger;

        public RegisterStudentCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockStudentService = new Mock<IStudentService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<RegisterStudentCommand>>();
            _mockValidationLogger = new Mock<IValidationLogger>();

            _handler = new RegisterStudentCommandHandler(
                _mockLogger.Object,
                _mockStudentService.Object,
                _mockMapper.Object,
                _mockValidator.Object,
                _mockValidationLogger.Object);
        }

        [Fact]
        public async Task HandleRequest_Should_Return_StudentId_When_Successful()
        {
            // Arrange
            var command = new RegisterStudentCommand { Name = "Test Student" };
            var student = new Student { StudentID = Guid.NewGuid(), Name = "Test Student" };

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterStudentCommand>()))
                .Returns(new ValidationResult());

            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<RegisterStudentCommand>()))
                       .Returns(student);

            _mockStudentService.Setup(ss => ss.RegisterStudent(student))
                               .Returns(Task.FromResult("Student registered"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be("Student registered");
        }

        [Fact]
        public void HandleRequest_Should_Throw_RequestValidationException_When_Validation_Fails()
        {
            // Arrange
            var command = new RegisterStudentCommand { Name = "Test Student" };
            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("StudentName", "StudentName is required.")
        };

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterStudentCommand>()))
                .Returns(new ValidationResult(validationFailures));

            _mockValidationLogger.Setup(vl => vl.LogValidationErrors(command, validationFailures))
                                .Returns("Validation failed");

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<RequestValidationException>().WithMessage("Validation failed");
        }

        [Fact]
        public async Task HandleRequest_Should_Call_StudentService_RegisterStudent()
        {
            // Arrange
            var command = new RegisterStudentCommand { Name = "Test Student" };
            var student = new Student { StudentID = Guid.NewGuid(), Name = "Test Student" };

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterStudentCommand>()))
                .Returns(new ValidationResult());

            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<RegisterStudentCommand>()))
                       .Returns(student);

            _mockStudentService.Setup(ss => ss.RegisterStudent(student))
                               .ReturnsAsync("Student registered");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockStudentService.Verify(ss => ss.RegisterStudent(student), Times.Once);
        }
    }


}
