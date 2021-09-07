using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels.Comment;
using ForumApp.ViewModels.Thread;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IChannel channelService;
        private readonly IThread threadService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IThreadFormatter threadFormatter;
        public ThreadController(IChannel channelService, IThread threadService, UserManager<ApplicationUser> userManager, IThreadFormatter threadFormatter)
        {
            this.channelService = channelService;
            this.threadService = threadService;
            this.userManager = userManager;
            this.threadFormatter = threadFormatter;
        }
        public async Task<IActionResult> Index(int id)
        {
            var thread = threadService.GetById(id);
            var comments = GetComments(thread).OrderBy(c => c.Posted);
            var threadViewModel = new ThreadViewModel
            {
                Id = thread.Id,
                Title = thread.Title,
                AuthorId = thread.Author.Id,
                AuthorName = thread.Author.UserName,
                AuthorImageLink = thread.Author.ProfileImageUrl,
                IsAuthorAdmin = userManager.GetRolesAsync(thread.Author)
                            .Result.Contains("Admin"),
                Posted = thread.CreateAt.ToString(CultureInfo.InvariantCulture),
                ThreadContent = threadFormatter.Prettify(thread.Content),
                ChannelId = thread.Channel.Id,
                ChannelName = thread.Channel.Title,
                ChannelImageLink = thread.Channel.ImageLink
            };

            var current = await userManager.GetUserAsync(User);

            var model = new ThreadIndexModel
            {
                IsCurrentUserActive = current?.IsActive ?? false,
                Thread = threadViewModel,
                Comments = GetComments(thread)
            };
            return View(model);
        }

        private IEnumerable<CommentViewModel> GetComments(Thread thread)
        {
            return thread.Comments.Select(c => new CommentViewModel
            {
                Id = c.Id,
                AuthorName = c.Author.UserName,
                AuthorId = c.Author.Id,
                AuthorImageLink = c.Author.ProfileImageUrl,
                Posted = c.CreateAt.ToString(CultureInfo.InvariantCulture),
                CommentContent = threadFormatter.Prettify(c.Content),
                IsAuthorAdmin = userManager.GetRolesAsync(c.Author)
                            .Result.Contains("Admin")
            });
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var channel = channelService.GetById(id);

            var model = new CreateThreadModel
            {
                ChannelName = channel.Title,
                ChannelId = channel.Id,
                AuthorName = User.Identity.Name,
                ChannelImageLink = channel.ImageLink
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateThreadModel model)
        {
            var userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);
            var channel = channelService.GetById(model.ChannelId);
            var post = new Thread
            {
                Title = model.Title,
                Content = model.Content,
                CreateAt = DateTime.Now,
                Channel = channel,
                Author = user,
                IsAvailable = false
            };

            await threadService.AddAsync(post);

            return RedirectToAction("Index", "Channel", model.ChannelId);
        }
    }
}
