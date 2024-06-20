using ACME.SchoolManagement.Core.Application.HandlerBase;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Models;
using AutoMapper;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryHandler : HandlerBase<ListOfCoursesAndStudentsByDateQuery, IList<EnrollmentModel>>
    {
        private ILoggerService? _logger;
        private IEnrollmentService? _enrollmentService;
        private IMapper? _mapper;

        public ListOfCoursesAndStudentsByDateQueryHandler(ILoggerService logger, 
            IEnrollmentService enrollmentService, 
            IMapper mapper, 
            IValidator<ListOfCoursesAndStudentsByDateQuery> validator,
            IValidationLogger validationLogger) : base(logger, validator, validationLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _enrollmentService = enrollmentService ?? throw new ArgumentNullException(nameof(enrollmentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<IList<EnrollmentModel>> HandleRequest(ListOfCoursesAndStudentsByDateQuery request, CancellationToken cancellationToken)
        {
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
    }
}
