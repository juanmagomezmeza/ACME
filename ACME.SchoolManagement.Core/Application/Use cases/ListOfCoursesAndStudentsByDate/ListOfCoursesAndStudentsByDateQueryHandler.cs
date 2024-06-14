using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Exceptions;
using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Application.Models;
using ACME.SchoolManagement.Core.Application.Request;
using AutoMapper;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryHandler : IRequestHandler<ListOfCoursesAndStudentsByDateQuery, IList<EnrollmentModel>>
    {
        private ILoggerService? _logger;
        private IEnrollmentService? _enrollmentService;
        private IMapper? _mapper;

        public async Task<IList<EnrollmentModel>> Handle(ListOfCoursesAndStudentsByDateQuery request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            LoadServices(serviceFactory);
            ValidateRequest(request);
            return await ListOfCoursesAndStudentsByDate(request.StartDate, request.EndDate);
        }

        public async Task<IList<EnrollmentModel>> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate)
        {
            var list = await _enrollmentService.ListOfCoursesAndStudentsByDate(startDate,endDate);
            List<EnrollmentModel> enrollmentModels = new List<EnrollmentModel>();
            foreach (var item in list)
            {
                var model = _mapper.Map<EnrollmentModel>(item);
                enrollmentModels.Add(model);
            }
            return enrollmentModels;
        }

        private void ValidateRequest(ListOfCoursesAndStudentsByDateQuery request)
        {
            var failures = new List<ValidationFailure>();
            var validator = new ListOfCoursesAndStudentsByDateQueryValidator();
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
        }
    }
}
