namespace ACME.SchoolManagement.Core.Domain.Exceptions
{
    public class RequestHandlerNotFoundException : Exception
    {
        public RequestHandlerNotFoundException(string requestName) : base($"Request Handler Not Found:{requestName}")
        {

        }
    }
}
