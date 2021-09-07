using ForumApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Interfaces
{
    public interface IThread
    {
        Task AddAsync(Thread thread);
        Task AddCommentAsync(Comment comment);
        int GetCommentCount(int id);
        Thread GetById(int id);
        IEnumerable<Thread> GetAll();
        IEnumerable<Thread> GetFilteredThreads(string query);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Thread> threads);
        IEnumerable<Thread> GetLatestThreads(int count);
        string GetChannelImageUrl(int id);
    }
}
