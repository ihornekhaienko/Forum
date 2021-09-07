using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class ThreadService : IThread
    {
        private readonly ApplicationDbContext db;

        public ThreadService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Thread thread)
        {
            db.Add(thread);
            await db.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Thread> GetAll()
        {
            var threads = db.Threads
                .Include(t => t.Channel)
                .Include(t => t.Author)
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Author);
            return threads;
        }

        public IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Thread> threads)
        {
            var users = new List<ApplicationUser>();

            foreach (var thread in threads)
            {
                users.Add(thread.Author);

                if (!thread.Comments.Any()) 
                    continue;

                users.AddRange(thread.Comments.Select(c => c.Author));
            }

            return users.Distinct();
        }

        public Thread GetById(int id)
        {
            return db.Threads.Where(t => t.Id == id)
                .Include(t => t.Channel)
                .Include(t => t.Author)
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefault();
        }

        public string GetChannelImageUrl(int id)
        {
            var thread = GetById(id);
            return thread.Channel.ImageLink;
        }

        public int GetCommentCount(int id)
        {
            return GetById(id).Comments.Count();
        }

        public IEnumerable<Thread> GetFilteredThreads(string query)
        {
            query = query.ToLower();

            return db.Threads
                .Include(t => t.Channel)
                .Include(t => t.Author)
                .Include(t => t.Comments)
                .Where(t => t.Title.ToLower().Contains(query) || t.Content.ToLower().Contains(query));
        }

        public IEnumerable<Thread> GetLatestThreads(int count)
        {
            var threads = GetAll().OrderByDescending(t => t.CreateAt);
            return threads.Take(count);
        }
    }
}
