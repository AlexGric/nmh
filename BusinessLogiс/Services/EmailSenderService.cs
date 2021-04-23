using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace BusinessLogic.Services
{
    public class EmailSenderService
    {
        private readonly IConfiguration config;

        public EmailSenderService(IConfiguration config)
        {
            this.config = config;
        }

        public void SendEmail(string to, string subject, string body)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("MailSettings")["Mail"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            smtp.Connect(
                config.GetSection("MailSettings")["Host"], 
                Convert.ToInt32(config.GetSection("MailSettings")["Port"]), 
                SecureSocketOptions.StartTls);
            
            smtp.Authenticate(config.GetSection("MailSettings")["Mail"], config.GetSection("MailSettings")["Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
