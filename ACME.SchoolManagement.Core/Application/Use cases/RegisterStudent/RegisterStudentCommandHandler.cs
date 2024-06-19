using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.HandlerBase;
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
        IValidator<RegisterStudentCommand> validator)
        : base(logger, validator)
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
