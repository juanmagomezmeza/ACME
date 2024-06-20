using ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.RegisterCourse
{
    public class RegisterCourseCommandHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<RegisterCourseCommand>> _mockValidator;
        private readonly RegisterCourseCommandHandler _handler;
        private readonly Mock<IValidationLogger> _mockValidationLogger;

        public RegisterCourseCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockCourseService = new Mock<ICourseService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<RegisterCourseCommand>>();
            _mockValidationLogger = new Mock<IValidationLogger>();


            _handler = new RegisterCourseCommandHandler(
                _mockLogger.Object,
                _mockCourseService.Object,
                _mockMapper.Object,
                _mockValidator.Object,
                _mockValidationLogger.Object);
        }

        [Fact]
        public async Task HandleRequest_Should_Return_CourseId_When_Successful()
        {
            // Arrange
            var command = new RegisterCourseCommand { Name = "Test Course" };
            var course = new Course { CourseID = Guid.NewGuid(), Name = "Test Course" };

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterCourseCommand>()))
                .Returns(new ValidationResult());

            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<RegisterCourseCommand>()))
                       .Returns(course);

            _mockCourseService.Setup(cs => cs.RegisterCourse(course))
                              .Returns(Task.FromResult("Course registered"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be("Course registered");
        }

        [Fact]
        public async Task HandleRequest_Should_Throw_RequestValidationException_When_Validation_FailsAsync()
        {
            // Arrange
            var command = new RegisterCourseCommand { Name = "Test Course" };
            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("CourseName", "CourseName is required.")
        };

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterCourseCommand>()))
                .Returns(new ValidationResult(validationFailures));

            _mockValidationLogger.Setup(l => l.LogValidationErrors(It.IsAny<RegisterCourseCommand>(), validationFailures))
                .Returns("Validation failed");

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<RequestValidationException>().WithMessage("Validation failed");
        }

        [Fact]
        public async Task RegisterCourse_Should_Call_CourseService_RegisterCourse()
        {
            // Arrange
            var command = new RegisterCourseCommand { Name = "Test Course" };
            var validationFailures = new List<ValidationFailure>();
            var course = new Course { CourseID = Guid.NewGuid(), Name = "Test Course" };

            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<RegisterCourseCommand>()))
                       .Returns(course);

            _mockCourseService.Setup(cs => cs.RegisterCourse(course))
                              .Returns(Task.FromResult("Course registered"));

            _mockValidator.Setup(v => v.Validate(It.IsAny<RegisterCourseCommand>()))
                .Returns(new ValidationResult(validationFailures));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockCourseService.Verify(cs => cs.RegisterCourse(course), Times.Once);
        }
    }

}

