using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.HandlerBase;
using AutoMapper;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse
{

    public class RegisterCourseCommandHandler : HandlerBase<RegisterCourseCommand, string?>
    {
        private readonly ILoggerService? _logger;
        private ICourseService? _courseService;
        private IMapper? _mapper;

        public RegisterCourseCommandHandler(ILoggerService logger, 
            ICourseService courseService, 
            IMapper mapper,
            IValidator<RegisterCourseCommand> validator,
            IValidationLogger validationLogger) : base(logger, validator, validationLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<string?> HandleRequest(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            return await RegisterCourse(request);
        }

        private async Task<string> RegisterCourse(RegisterCourseCommand command)
        {
            var course = _mapper.Map<Course>(command);
            return await _courseService.RegisterCourse(course);
        }
    }
}
