namespace ACME.SchoolManagement.Core.Application.Models
{
    public class ValidationErrorModel
    {
        public string Message { set; get; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
