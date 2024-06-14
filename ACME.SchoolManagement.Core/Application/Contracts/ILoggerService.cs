namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface ILoggerService
    {
        void Error(string message, object value);
        void Information(string message);
        void Warning(string message);

    }
}
