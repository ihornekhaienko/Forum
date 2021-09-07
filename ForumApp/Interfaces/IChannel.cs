using ForumApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Interfaces
{
    public interface IChannel
    {
        Channel GetById(int id);
        IEnumerable<Channel> GetAll();
        Thread GetLatestThread(int channelId);
        IEnumerable<ApplicationUser> GetActiveUsers(int channelId);
        bool HasRecentPost(int id);
        Task AddAsync(Channel channel);
        IEnumerable<Thread> GetFilteredThreads(int channelId, string query);
    }
}
