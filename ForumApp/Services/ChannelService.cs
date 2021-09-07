using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class ChannelService : IChannel
    {
        private readonly ApplicationDbContext db;
        private readonly IThread threadService;
        public ChannelService(ApplicationDbContext db, IThread threadService)
        {
            this.db = db;
            this.threadService = threadService;
        }

        public async Task AddAsync(Channel channel)
        {
            db.Add(channel);
            await db.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int channelId)
        {
            var threads = GetById(channelId).Threads.Where(t => t.IsAvailable);

            if (threads == null || !threads.Any())
            {
                return new List<ApplicationUser>();
            }

            return threadService.GetAllUsers(threads);
        }

        public IEnumerable<Channel> GetAll()
        {
            return db.Channels
                .Include(c => c.Threads);
        }

        public Channel GetById(int id)
        {
            var channel = db.Channels
                 .Where(c => c.Id == id)
                    .Include(c => c.Threads)
                        .ThenInclude(c => c.Author)
                    .Include(c => c.Threads)
                        .ThenInclude(c => c.Comments)
                            .ThenInclude(c => c.Author)
                    .Include(c => c.Threads)
                        .ThenInclude(c => c.Channel)
                 .FirstOrDefault();

            if (channel.Threads == null)
            {
                channel.Threads = new List<Thread>();
            }

            return channel;
        }

        public IEnumerable<Thread> GetFilteredThreads(int channelId, string query)
        {
            if (channelId == 0) 
                return threadService.GetFilteredThreads(query);

            var channel = GetById(channelId);

            return string.IsNullOrEmpty(query)
                ? channel.Threads
                : channel.Threads.Where(t
                    => t.Title.Contains(query) || t.Content.Contains(query));
        }

        public Thread GetLatestThread(int channelId)
        {
            var threads = GetById(channelId).Threads;

            if (threads != null)
            {
                return GetById(channelId).Threads
                    .OrderByDescending(t => t.CreateAt)
                    .FirstOrDefault();
            }

            return new Thread();
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);
            return GetById(id).Threads.Any(t => t.CreateAt >= window);
        }
    }
}
