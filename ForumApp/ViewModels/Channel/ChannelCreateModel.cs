using ForumApp.Models;
using Microsoft.AspNetCore.Http;

namespace ForumApp.ViewModels.Channel
{
    public class ChannelCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public IFormFile ImageUpload { get; set; }
    }
}
