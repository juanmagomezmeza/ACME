namespace ACME.SchoolManagement.Core.Application.Exceptions
{
    public class RequestHandlerNotFoundException : Exception
    {
        public RequestHandlerNotFoundException(string requestName) : base($"Request Handler Not Found:{requestName}")
        {

        }
    }
}
