using ACME.SchoolManagement.Core.Domain.Contracts.Services;

namespace ACME.SchoolManagement.Infrastructure.PaymentGateway
{
    public class PaymentGateway : IPaymentGateway
    {
        public async Task<bool> ProcessPaymentAsync(Guid studentId, Guid courseId, decimal amount)
        {
            return await Task.FromResult(true);
        }
    }
}
