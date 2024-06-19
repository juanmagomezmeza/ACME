using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
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
        private readonly RegisterStudentCommandHandler _handler;

        public RegisterStudentCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockStudentService = new Mock<IStudentService>();
            _mockMapper = new Mock<IMapper>();

            _handler = new RegisterStudentCommandHandler(
                _mockLogger.Object,
                _mockStudentService.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_StudentId_When_Successful()
        {
            // Arrange
            var command = new RegisterStudentCommand
            {
                // Fill properties as needed
            };

            var student = new Student();
            var expectedStudentId = "studentId123";

            _mockMapper.Setup(m => m.Map<Student>(command)).Returns(student);
            _mockStudentService.Setup(ss => ss.RegisterStudent(student)).ReturnsAsync(expectedStudentId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(expectedStudentId);
        }
    }

}
