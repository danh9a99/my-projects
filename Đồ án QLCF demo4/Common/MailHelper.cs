using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MailHelper
    {
        //public void SendMail(string toEmailAddress, string subject, string content)
        //{
        //    var fromEmailAddress = ConfigurationManager.AppSettings["ngothanhdanhtqt@gmail.com"].ToString();
        //    var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
        //    var fromEmailPassword = ConfigurationManager.AppSettings["ngothanhdanh1221112"].ToString();
        //    var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        //    var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

        //    bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

        //    string body = content;
        //    MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
        //    message.Subject = subject;
        //    message.IsBodyHtml = true;
        //    message.Body = body;

        //    var client = new SmtpClient();
        //    client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
        //    client.Host = smtpHost;
        //    client.EnableSsl = enabledSsl;
        //    client.Port = !string.IsNullOrEmpty(smtpHost) ? Convert.ToInt32(smtpPort) : 0;
        //    client.Send(message);
        //}
        public void SendEmail(string address, string subject, string message)
        {
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("ngothanhdanhtqt@gmail.com", "ngothanhdanh1221112"),
                EnableSsl = true

            };
            
            var mess = new MailMessage();
            mess.To.Add(new MailAddress(address));
            mess.From = new MailAddress("ngothanhdanhtqt@gmail.com");
            mess.Body = message;
            mess.Subject = subject;

            mail.Send(mess);
        }
    }
}
