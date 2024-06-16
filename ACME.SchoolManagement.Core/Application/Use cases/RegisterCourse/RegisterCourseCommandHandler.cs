﻿using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Exceptions;
using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Application.Request;
using ACME.SchoolManagement.Core.Entities;
using AutoMapper;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse
{

    public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand, string?>
    {
        private ILoggerService? _logger;
        private ICourseService? _courseService;
        private IMapper? _mapper;

        public async Task<string?> Handle(RegisterCourseCommand request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            LoadServices(serviceFactory);
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

        private void LoadServices(ServiceFactory serviceFactory)
        {
            _logger = this.GetLoggerService(serviceFactory);
            _courseService=this.GetCourseService(serviceFactory);
            _mapper=this.GetAutomapperService(serviceFactory);
        }
    }
}
