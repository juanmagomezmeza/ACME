using ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace ACME.SchoolManagement.Core.Tests.Application.RegisterCourse
{
    public class RegisterCourseCommandHandlerTests
    {
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RegisterCourseCommandHandler _handler;

        public RegisterCourseCommandHandlerTests()
        {
            _mockLogger = new Mock<ILoggerService>();
            _mockCourseService = new Mock<ICourseService>();
            _mockMapper = new Mock<IMapper>();

            _handler = new RegisterCourseCommandHandler(
                _mockLogger.Object,
                _mockCourseService.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_CourseId_When_Successful()
        {
            // Arrange
            var command = new RegisterCourseCommand
            {
                // Fill properties as needed
            };

            var course = new Course();
            var expectedCourseId = "courseId123";

            _mockMapper.Setup(m => m.Map<Course>(command)).Returns(course);
            _mockCourseService.Setup(cs => cs.RegisterCourse(course)).ReturnsAsync(expectedCourseId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(expectedCourseId);
        }
    }
}

