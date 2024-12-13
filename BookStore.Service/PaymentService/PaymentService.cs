using BookStore.Core.Send_Mail;
using BookStore.Core.Service.Contract;
using System.Net.Mail;
using System.Net;


namespace BookStore.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        public void SendEmailToCustomer(int orderId, string email)
        {
            string fromMail = "shehabsameh987123@gmail.com";
            string fromPassword = "fktn ivsn rnix dpjj";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "payment status";
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> <div>\r\n <h1 style=\"color: green;\">payment successfully made for order number 66</h1>\r\n    <h3>for more infomation email us : shehabsameh987123&#64;gmail.com</h3>\r\n</div> </body></html>";
            message.IsBodyHtml = true;
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtp.Send(message);
        }
    }
}
