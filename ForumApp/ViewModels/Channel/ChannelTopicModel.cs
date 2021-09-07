using ForumApp.ViewModels.Thread;
using System.Collections.Generic;

namespace ForumApp.ViewModels.Channel
{
    public class ChannelTopicModel
    {
        public bool IsCurrentUserActive { get; set; }
        public ChannelViewModel Channel { get; set; }
        public IEnumerable<ThreadViewModel> Threads { get; set; }
        public string Query { get; set; }
        public bool HasResults { get; set; }
    }
}
