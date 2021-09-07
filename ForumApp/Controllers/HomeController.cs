using ForumApp.Interfaces;
using ForumApp.ViewModels.Home;
using ForumApp.ViewModels.Thread;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThread threadService;
        public HomeController(IThread threadService)
        {
            this.threadService = threadService;
        }

        public IActionResult Index()
        {
            var latest = threadService.GetLatestThreads(10);
            var threads = latest.Select(post => new ThreadViewModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.Author.UserName,
                AuthorId = post.Author.Id,
                Posted = post.CreateAt.ToString(),
                CommentsCount = threadService.GetCommentCount(post.Id),
                ChannelName = post.Channel.Title,
                ChannelImageLink = threadService.GetChannelImageUrl(post.Id),
                ChannelId = post.Channel.Id
            });

            var model = new IndexViewModel
            {
                Threads = threads
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            return RedirectToAction("Topic", "Channel", new { query });
        }
    }
}
