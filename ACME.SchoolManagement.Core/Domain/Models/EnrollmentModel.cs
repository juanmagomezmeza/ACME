namespace ACME.SchoolManagement.Core.Domain.Models
{
    public class EnrollmentModel
    {
        public Guid CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
}
