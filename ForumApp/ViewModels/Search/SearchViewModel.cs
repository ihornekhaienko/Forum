using ForumApp.ViewModels.Thread;
using System.Collections.Generic;

namespace ForumApp.ViewModels.Search
{
    public class SearchViewModel
    {
        public IEnumerable<ThreadViewModel> Threads { get; set; }
        public string Query { get; set; }
        public bool HasResults { get; set; }
    }
}
