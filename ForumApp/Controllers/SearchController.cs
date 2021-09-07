using ForumApp.Interfaces;
using ForumApp.ViewModels.Search;
using ForumApp.ViewModels.Thread;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;

namespace ForumApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IThread threadService;

        public SearchController(IThread threadService)
        {
            this.threadService = threadService;
        }

        public IActionResult Results(string query)
        {
            var threads = threadService.GetFilteredThreads(query).ToList();
            var hasResults = (!string.IsNullOrEmpty(query) && !threads.Any());

            var threadViewModel = threads.Select(t => new ThreadViewModel
            {
                Id = t.Id,
                ChannelId = t.Channel.Id,
                ChannelImageLink = t.Channel.ImageLink,
                ChannelName = t.Channel.Title,
                AuthorName = t.Author.UserName,
                AuthorId = t.Author.Id,
                Title = t.Title,
                Posted = t.CreateAt.ToString(CultureInfo.InvariantCulture),
                CommentsCount = t.Comments.Count()
            }).OrderByDescending(t => t.Posted);

            var model = new SearchViewModel
            {
                HasResults = hasResults,
                Threads = threadViewModel,
                Query = query,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            return RedirectToAction("Results", new { query });
        }
    }
}
