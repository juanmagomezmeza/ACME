namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface ILoggerService
    {
        void Error(string message, object value);
        void Information(string message);
        void Warning(string message);

    }
}
