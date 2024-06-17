﻿using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Application.Services.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using AutoMapper;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse
{
    public class ContractCourseCommandHandler : IRequestHandler<ContractCourseCommand, string?>
    {
        private ILoggerService? _logger;
        private IEnrollmentService? _enrollmentService;
        private IMapper? _mapper;
        private IPaymentGateway _paymentGateway;

        public async Task<string?> Handle(ContractCourseCommand request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            LoadServices(serviceFactory);
            ValidateRequest(request);
            return await ContractCourse(request);
        }

        private async Task<string> ContractCourse(ContractCourseCommand command)
        {
            if (await _paymentGateway.ProcessPaymentAsync(command.StudentID, command.CourseID, 234234) is false)
                throw new UnauthorizedAccessException("Paid error");

            if (await ValidateDataConsistency(command) is false)
                throw new InvalidDataException("Invalid data");

            var enrollment = _mapper.Map<Enrollment>(command);
            return await _enrollmentService.Enroll(enrollment);
        }

        private async Task<bool> ValidateDataConsistency(ContractCourseCommand command)
        {
            if (await _enrollmentService.CourseExistsAsync(command.CourseID) is false)
                return false;
            if (await _enrollmentService.StudentExistsAsync(command.StudentID) is false)
                return false;
            return true;
        }

        private void ValidateRequest(ContractCourseCommand request)
        {
            var failures = new List<ValidationFailure>();
            var validator = new ContractCourseCommandValidator();
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
            _enrollmentService = this.GetEnrollmentService(serviceFactory);
            _mapper = this.GetAutomapperService(serviceFactory);
            _paymentGateway=this.GetIPaymentGatewayService(serviceFactory);
        }
    }
}
