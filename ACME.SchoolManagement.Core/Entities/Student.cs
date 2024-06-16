namespace ACME.SchoolManagement.Core.Entities
{
    public class Student
    {
        public Guid StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
