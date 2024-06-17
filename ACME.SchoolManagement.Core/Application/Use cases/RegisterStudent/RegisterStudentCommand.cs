using ACME.SchoolManagement.Core.Domain.Contracts.Request;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent
{
    public class RegisterStudentCommand : IRequest<string?>
    {
        public string Name { get; set; }
        public int Age { get; set; } = 0;
    }
}
