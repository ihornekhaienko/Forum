using ForumApp.ViewModels.Thread;
using System.Collections.Generic;

namespace ForumApp.ViewModels.Home
{
    public class IndexViewModel
    {
        public string Query { get; set; }
        public IEnumerable<ThreadViewModel> Threads { get; set; }
    }
}
