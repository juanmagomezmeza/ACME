namespace ACME.SchoolManagement.Core.Domain.Models
{
    public class CourseModel
    {
        public CourseModel(string name, decimal registrationFee, DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate) throw new ArgumentException("End date must be after start date.");
            Name = name;
            RegistrationFee = registrationFee;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int CourseID { get; set; }
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
