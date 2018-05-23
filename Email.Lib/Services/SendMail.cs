using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Email.Lib.Entities;

namespace Email.Lib.Services
{
    public class SendMail
    {
        public static bool SendMessage(MailingComponents mailingComponents)
        {

            bool mailSent = false;

            MailAddress from = new MailAddress(mailingComponents.From);

            MailAddress to = new MailAddress(mailingComponents.To);
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;
            message.Subject = mailingComponents.Subject;
            if (!String.IsNullOrEmpty(mailingComponents.Cc))
            {
                message.CC.Add(mailingComponents.Cc);
            }
            if (!String.IsNullOrEmpty(mailingComponents.Bcc))
            {
                message.CC.Add(mailingComponents.Bcc);
            }
            message.Body = mailingComponents.HtmlBody;
            message.Subject = mailingComponents.Subject;
            SmtpClient client = new SmtpClient(mailingComponents.SmtpServer);

            try
            {
                client.Port = mailingComponents.Port;
                client.Credentials = new System.Net.NetworkCredential(mailingComponents.From, mailingComponents.PassWord);
                client.EnableSsl = true;

                client.Send(message);
                mailSent = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage(): {0}",
                      ex.ToString());
            }
            return mailSent;
        }
    }
}
