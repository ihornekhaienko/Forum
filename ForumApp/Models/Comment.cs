using System;

namespace ForumApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Thread Thread { get; set; }
        public bool IsAvailable { get; set; }
    }
}
