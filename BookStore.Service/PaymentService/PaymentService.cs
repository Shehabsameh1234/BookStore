using BookStore.Core.Send_Mail;
using BookStore.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        public Email SendEmailToCustomer(int orderId, string email)
        {
            var sendEmail = new Email()
            {
                Title = "Update Order Status",
                To = email,
                Body = $"you order number {orderId} has been placed",
            };
            EmailSetting.SendEmail(sendEmail);
            return sendEmail;
        }
    }
}
