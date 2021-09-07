using System;
using System.Collections.Generic;

namespace ForumApp.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime CreateAt { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public Channel Channel { get; set; }

        public bool IsAvailable { get; set; }
    }
}
