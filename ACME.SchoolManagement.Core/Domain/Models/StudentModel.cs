namespace ACME.SchoolManagement.Core.Domain.Models
{
    public class StudentModel
    {
        public StudentModel(string name, int age)
        {
            if (age < 18) throw new ArgumentException("Only adults can register.");
            Name = name;
            Age = age;
        }

        public int StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
