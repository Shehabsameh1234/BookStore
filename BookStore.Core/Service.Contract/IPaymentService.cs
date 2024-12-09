using BookStore.Core.Send_Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Service.Contract
{
    public interface IPaymentService
    {
        Email SendEmailToCustomer(int orderId, string email);
    }
}
