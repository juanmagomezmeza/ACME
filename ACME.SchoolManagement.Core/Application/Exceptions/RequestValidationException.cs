using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Exceptions
{
    public class RequestValidationException : Exception
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; private set; }
        public RequestValidationException(string message, IList<ValidationFailure> errors) : base(message)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
            var gpErrors = errors.GroupBy(ve => ve.PropertyName);
            foreach (var er in gpErrors)
                Errors.Add(er.Key, er.Select(ve => ve.ErrorMessage).ToList());
        }

        public RequestValidationException(string message, IDictionary<string, IEnumerable<string>> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
