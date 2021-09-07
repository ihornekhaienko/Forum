namespace ForumApp.ViewModels.Thread
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageLink { get; set; }
        public bool IsAuthorAdmin { get; set; }
        public string Posted { get; set; }
        public string ThreadContent { get; set; }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelImageLink { get; set; }

        public int CommentsCount { get; set; }
    }
}
