namespace ACME.SchoolManagement.Core.Application.Entities
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
