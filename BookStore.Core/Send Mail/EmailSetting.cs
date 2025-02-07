using System.Net.Mail;
using System.Net;


namespace BookStore.Core.Send_Mail
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("shehabsameh987123@gmail.com", "fktn ivsn rnix dpjj");
            client.Send("shehabsameh987123@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
