using System;

namespace ForumApp.ViewModels.Thread
{
    public class CreateThreadModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ChannelName { get; set; }
        public string ChannelImageLink { get; set; }
        public int ChannelId { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
