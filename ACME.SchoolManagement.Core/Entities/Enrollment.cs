namespace ACME.SchoolManagement.Core.Entities
{
    public class Enrollment
    {
        public Guid CourseID { get; set; }
        public Course Course { get; set; }

        public Guid StudentID { get; set; }
        public Student Student { get; set; }
    }
}
