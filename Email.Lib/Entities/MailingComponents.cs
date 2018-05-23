using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Lib.Entities
{
    public class MailingComponents
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }

        public MailingComponents(string _from, string _to, string _subject, string _htmlBody, string _username, string paswoord, int poort, string _smtpServer)
        {
            From = _from;
            To = _to;
            Subject = _subject;
            HtmlBody = _htmlBody;
            UserName = _username;
            PassWord = paswoord;
            Port = poort;
            SmtpServer = _smtpServer;
        }

        public MailingComponents(string _from, string _to, string _subject, string _htmlBody, string _username, string paswoord)
        {
            From = _from;
            To = _to;
            Subject = _subject;
            HtmlBody = _htmlBody;
            UserName = _username;
            PassWord = paswoord;
            Port = 587;
            SmtpServer = "smtp.gmail.com";
        }
    }
}
