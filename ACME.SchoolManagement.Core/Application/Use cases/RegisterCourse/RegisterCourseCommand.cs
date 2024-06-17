using ACME.SchoolManagement.Core.Domain.Contracts.Request;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse
{
    public class RegisterCourseCommand : IRequest<string?>
    {
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
