using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse
{

    public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand, string?>
    {
        private readonly ILoggerService? _logger;
        private ICourseService? _courseService;
        private IMapper? _mapper;

        public RegisterCourseCommandHandler(ILoggerService logger, ICourseService courseService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string?> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);
            return await RegisterCourse(request);
        }

        private async Task<string> RegisterCourse(RegisterCourseCommand command)
        {
            var course = _mapper.Map<Course>(command);
            return await _courseService.RegisterCourse(course);
        }

        private void ValidateRequest(RegisterCourseCommand request)
        {
            var failures = new List<ValidationFailure>();
            var validator = new RegisterCourseCommandValidator();
            var validationResults = validator.Validate(request);

            if (!validationResults.IsValid)
                failures = validationResults.Errors.ToList();

            if (failures.Any())
            {
                string? message = _logger?.LogValidationErrors(request, failures);
                throw new RequestValidationException(message, failures);
            }
        }
    }
}
