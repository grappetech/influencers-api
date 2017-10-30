using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Action.Services.SMTP
{
    public class SmtpService
    {
        public static void SendMessage(string receiver, string subject, string body)
        {
            try
            {
                var config = SmtpConfiguration.GetConfiguration();

                SmtpClient client = new SmtpClient(config.Host);
                client.Port = config.Port;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(config.UserName, config.Password);
                client.EnableSsl = config.IsSSL;
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(config.Sender);
                mailMessage.To.Add(receiver);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}