using ForumApp.ViewModels.Comment;
using System.Collections.Generic;

namespace ForumApp.ViewModels.Thread
{
    public class ThreadIndexModel
    {
        public bool IsCurrentUserActive { get; set; }
        public ThreadViewModel Thread { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
