using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string name, string email, string subject, string message, IConfiguration configuration);
    }
}
