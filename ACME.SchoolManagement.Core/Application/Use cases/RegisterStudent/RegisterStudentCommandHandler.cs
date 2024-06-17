using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Application.Services.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, string?>
    {
        private ILoggerService? _logger;
        private IStudentService? _studentService;
        private IMapper? _mapper;

        public async Task<string> Handle(RegisterStudentCommand request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            LoadServices(serviceFactory);
            ValidateRequest(request);
            return await RegisterStudent(request);
        }

        private async Task<string> RegisterStudent(RegisterStudentCommand command)
        {
            var student = _mapper.Map<Student>(command);
            return await _studentService.RegisterStudent(student);
        }
            
        private void ValidateRequest(RegisterStudentCommand request)
        {
            var failures = new List<ValidationFailure>();
            var validator = new RegisterStudentCommandValidator();
            var validationResults = validator.Validate(request);

            if (!validationResults.IsValid)
                failures = validationResults.Errors.ToList();

            if (failures.Any())
            {
                string? message = _logger?.LogValidationErrors(request, failures);
                throw new RequestValidationException(message, failures);
            }
        }

        private void LoadServices(ServiceFactory serviceFactory)
        {
            _logger = this.GetLoggerService(serviceFactory);
            _studentService = this.GetStudentService(serviceFactory);
            _mapper = this.GetAutomapperService(serviceFactory);
        }
    }
}
