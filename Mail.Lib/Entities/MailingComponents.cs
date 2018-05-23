using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingList.Lib.Entities
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
    }
}
