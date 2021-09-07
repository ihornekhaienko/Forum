using ForumApp.ViewModels.Thread;
using System.Collections.Generic;

namespace ForumApp.ViewModels.Channel
{
    public class ChannelViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string CreateAt { get; set; }
        public int ThreadCount { get; set; }
        public int UserCount { get; set; }
        public bool HasRecentPost { get; set; }

        public ThreadViewModel Latest { get; set; }
        public IEnumerable<ThreadViewModel> Threads { get; set; }
    }
}
