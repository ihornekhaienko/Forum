using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ForumApp.Services
{
    public class CommentService : IComment
    {
        private readonly ApplicationDbContext db;

        public CommentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Comment GetById(int id)
        {
            return db.Comments
                .Include(c => c.Thread)
                    .ThenInclude(t => t.Channel)
                .Include(c => c.Thread)
                    .ThenInclude(t => t.Author)
                    .First(c => c.Id == id);
        }
    }
}
