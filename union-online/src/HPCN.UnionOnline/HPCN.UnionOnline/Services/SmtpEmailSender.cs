using Microsoft.Extensions.Logging;
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

        public Task SendEmailAsync(string to, string subject, string message)
        {
            _logger.LogInformation(1, $"EMAIL: {subject} sent to {to}");
            return Task.FromResult(0);
        }
    }
}
