using ACME.SchoolManagement.Core.Application.HandlerBase;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Entities;
using AutoMapper;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent
{
    public class RegisterStudentCommandHandler : HandlerBase<RegisterStudentCommand, string?>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public RegisterStudentCommandHandler(
        ILoggerService logger,
        IStudentService studentService,
        IMapper mapper,
        IValidator<RegisterStudentCommand> validator,
            IValidationLogger validationLogger)
        : base(logger, validator, validationLogger)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<string?> HandleRequest(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            return await _studentService.RegisterStudent(student);
        }
    }
}
