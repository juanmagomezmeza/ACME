namespace ACME.SchoolManagement.Core.Domain.Models
{
    public class ValidationErrorModel
    {
        public string Message { set; get; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
