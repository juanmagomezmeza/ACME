namespace ACME.SchoolManagement.Core.Application.Request
{
    public interface IRequestDispatcher
    {
        /// <summary>
        /// Generic method to send information
        /// </summary>
        /// <typeparam name="TRequest">Request DTO</typeparam>
        /// <typeparam name="TResponse">Response DTO</typeparam>
        /// <param name="request">Request object</param>
        /// <param name="cancellationToken">Cancelation param</param>
        /// <returns></returns>
        Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>;
    }
}
