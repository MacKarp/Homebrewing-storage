using System.Diagnostics;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Backend.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IEmailConfiguration emailConfiguration, ILogger<EmailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }

        public void Send(EmailMessage emailMessage)
        {
            //message create, adding from and to addresses
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfiguration.SmtpUserName.Remove(_emailConfiguration.SmtpUserName.IndexOf("@"), _emailConfiguration.SmtpUserName.Length - _emailConfiguration.SmtpUserName.IndexOf("@")), _emailConfiguration.SmtpUserName));
            message.To.Add(new MailboxAddress(emailMessage.ToAddress.Name, emailMessage.ToAddress.Address));

            //adding message subject and body
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                Debug.WriteLine(_emailConfiguration.SmtpServer);
                Debug.WriteLine(_emailConfiguration.SmtpPort);
                Debug.WriteLine(_emailConfiguration.Ssl);
                try
                {
                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort,
                    _emailConfiguration.Ssl);
                    emailClient.Authenticate(_emailConfiguration.SmtpUserName, _emailConfiguration.SmtpPassword);
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError("Email sending service error. Exception: {exException}", ex.Message);
                }
                
            }

        }
    }
}