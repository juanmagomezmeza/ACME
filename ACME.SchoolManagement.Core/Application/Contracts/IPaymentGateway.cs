namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPaymentAsync(Guid studentId, Guid courseId, decimal amount);
    }
}
