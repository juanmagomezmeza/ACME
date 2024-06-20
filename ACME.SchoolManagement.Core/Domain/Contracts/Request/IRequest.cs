namespace ACME.SchoolManagement.Core.Domain.Contracts.Request
{
    /// <summary>
    /// Marker interface to represent a request with a response
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    public interface IRequest<out TResponse> { }
}
