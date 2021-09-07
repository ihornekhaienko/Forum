using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels.Comment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IChannel channelService;
        private readonly IThread threadService;
        private readonly UserManager<ApplicationUser> userManager;
        public CommentController(IChannel channelService, IThread threadService, UserManager<ApplicationUser> userManager)
        {
            this.channelService = channelService;
            this.threadService = threadService;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var thread = threadService.GetById(id);
            var channel = channelService.GetById(thread.Channel.Id);
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var model = new CommentViewModel
            {
                ThreadContent = thread.Content,
                ThreadTitle = thread.Title,
                ThreadId = thread.Id,

                ChannelName = channel.Title,
                ChannelId = channel.Id,
                ChannelImageLink = channel.ImageLink,

                AuthorName = User.Identity.Name,
                AuthorImageLink = user.ProfileImageUrl,
                AuthorId = user.Id,
                IsAuthorAdmin = user.IsAdmin,

                Posted = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);
            var thread = threadService.GetById(model.ThreadId);

            var comment = new Comment
            {
                Thread = thread,
                Content = model.CommentContent,
                CreateAt = DateTime.Now,
                Author = user
            };
            await threadService.AddCommentAsync(comment);

            return RedirectToAction("Index", "Thread", new { id = model.ThreadId });
        }
    }
}
