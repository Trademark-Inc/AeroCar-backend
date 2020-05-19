using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AeroCar.Utility
{
    public class EmailUtility
    {
        public static void SendEmail(string to, string subj, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("trademarkinctm@gmail.com");
            mail.To.Add(to);
            mail.Subject = subj;
            mail.Body = body;

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("trademarkinctm@gmail.com", "trademarktm");

            SmtpServer.Send(mail);
        }
    }
}
