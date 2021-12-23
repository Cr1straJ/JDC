using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;

namespace JDC.BusinessLogic.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string name, string email, string subject, string message, IConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}
