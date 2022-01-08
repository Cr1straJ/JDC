using System.Threading.Tasks;

namespace JDC.BusinessLogic.Utilities.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string name, string email, string subject, string message);
    }
}
