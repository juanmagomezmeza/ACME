namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPaymentAsync(Guid studentId, Guid courseId, decimal amount);
    }
}
