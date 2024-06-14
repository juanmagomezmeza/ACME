using FluentValidation.Results;
using Newtonsoft.Json;
using System.Text;

namespace ACME.SchoolManagement.Core.Application.Extensions
{
    public static class ValidationFailuresExtension
    {
        public static string ToJsonString(this IEnumerable<ValidationFailure> failures)
        {
            var sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            //sb.Append();
            return sb.ToString();
        }

        public static IDictionary<string, IEnumerable<string>> ToDictionary(this IEnumerable<ValidationFailure> failures)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var gpErrors = failures.GroupBy(ve => ve.PropertyName);
            foreach (var er in gpErrors)
                errors.Add(er.Key, er.Select(ve => ve.ErrorMessage).ToList());
            return errors;
        }

        public static string ToErrorString(this IEnumerable<ValidationFailure> failures)
        {
            var gpErrors = failures.GroupBy(ve => ve.PropertyName);
            var sb = new StringBuilder();
            sb.AppendLine(JsonConvert.SerializeObject(failures.ToDictionary()));
            return sb.ToString();
        }

    }
}
