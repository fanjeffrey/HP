using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public SmtpEmailSender(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SmtpEmailSender>();
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HP BF Union", "hpiunionbf@hp.com"));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp2.hp.com", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

            _logger.LogInformation(1, $"EMAIL: {subject} sent to {to}");
        }
    }
}
