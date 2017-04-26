using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
}
