

namespace BookStore.Core.Service.Contract
{
    public interface IPaymentService
    {
        void SendEmailToCustomer(int orderId, string email);
    }
}
