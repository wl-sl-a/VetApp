using System.Net;
using System.Net.Mail;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class EmailService: IEmailService
    {
        public void Send(string username, string password)
        {
            MailAddress from = new MailAddress("appvet390@gmail.com", "Vet App");
            MailAddress to = new MailAddress("anastasiia.solianyk@nure.ua");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Username and Password for VetApp";
            m.Body = "<p> Username:  " + username + "</p><p> Password:  " + password+ "</p>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("appvet390@gmail.com", "jvcmeytgaobkmtzm");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
