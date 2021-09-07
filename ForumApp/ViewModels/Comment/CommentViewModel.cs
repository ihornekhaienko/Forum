namespace ForumApp.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageLink { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public string Posted { get; set; }
        public string CommentContent { get; set; }

        public int ThreadId { get; set; }
        public string ThreadTitle { get; set; }
        public string ThreadContent { get; set; }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelImageLink { get; set; }
    }
}
