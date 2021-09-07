using System.Threading.Tasks;

namespace ForumApp.Interfaces
{
    public interface IEmail
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
