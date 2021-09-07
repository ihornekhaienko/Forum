using System;
using System.Collections.Generic;

namespace ForumApp.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public string ImageLink { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public IEnumerable<Thread> Threads { get; set; }
        public bool IsAvailable { get; set; }
    }
}
