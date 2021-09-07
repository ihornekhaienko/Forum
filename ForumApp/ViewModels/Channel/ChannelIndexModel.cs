using System.Collections.Generic;

namespace ForumApp.ViewModels.Channel
{
    public class ChannelIndexModel
    {
        public int ChannelCount { get; set; }
        public IEnumerable<ChannelViewModel> Channels { get; set; }
    }
}
