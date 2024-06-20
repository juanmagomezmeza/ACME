namespace ACME.SchoolManagement.Core.Domain.Exceptions
{
    public class DataConsistencyException : Exception
    {
        public DataConsistencyException(string message) : base(message) { }
    }
}
