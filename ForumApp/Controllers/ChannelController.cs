using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels.Channel;
using ForumApp.ViewModels.Thread;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IWebHostEnvironment appEnvironment;
        private readonly IChannel channelService;
        private readonly IFile fileService;
        private readonly UserManager<ApplicationUser> userManager;
        public ChannelController(IWebHostEnvironment appEnvironment, IChannel channelService, IFile fileService, UserManager<ApplicationUser> userManager)
        {
            this.appEnvironment = appEnvironment;
            this.channelService = channelService;
            this.fileService = fileService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var channels = channelService.GetAll().Select(c => new ChannelViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImageLink = c.ImageLink,
                CreateAt = c.CreateAt.ToString(CultureInfo.InvariantCulture),
                ThreadCount = c.Threads?.Count() ?? 0,
                UserCount = channelService.GetActiveUsers(c.Id).Count(),
                HasRecentPost = channelService.HasRecentPost(c.Id),
                Latest = GetLatestThread(c.Id)
            }).ToList();

            var model = new ChannelIndexModel
            {
                ChannelCount = channels.Count,
                Channels = channels
            };

            return View(model);
        }

        public async Task<IActionResult> Topic(int id, string query)
        {
            var channel = channelService.GetById(id);
            var threads = channelService.GetFilteredThreads(id, query).ToList();
            var hasResults = (!string.IsNullOrEmpty(query) && !threads.Any());

            var threadViewModel = threads.Select(t => new ThreadViewModel
            {
                Id = t.Id,
                AuthorName = t.Author.UserName,
                AuthorId = t.Author.Id,
                AuthorImageLink = t.Author.Id,
                Title = t.Title,
                Posted = t.CreateAt.ToString(CultureInfo.InvariantCulture),
                CommentsCount = t.Comments.Count()
            }).OrderByDescending(t => t.Posted);

            var current = await userManager.GetUserAsync(User);

            var model = new ChannelTopicModel
            {
                IsCurrentUserActive = current?.IsActive ?? false,
                HasResults = hasResults,
                Query = query,
                Threads = threadViewModel,
                Channel = new ChannelViewModel
                {
                    Id = channel.Id,
                    ImageLink = channel.ImageLink,
                    Title = channel.Title,
                    Description = channel.Description
                }
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ChannelCreateModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChannelCreateModel model)
        {
            string imageLink;
            if (model.ImageUpload != null)
            {
                imageLink = await AddFile(model.ImageUpload);
            }
            else
            {
                imageLink = "/images/channel.png";
            }

            var channel = new Channel()
            {
                Title = model.Title,
                Description = model.Description,
                CreateAt = DateTime.Now,
                ImageLink = imageLink,
                IsAvailable = true,
                Author = await userManager.FindByNameAsync(this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

            await channelService.AddAsync(channel);
            return RedirectToAction("Topic", "Channel", new { id = channel.Id } );
        }

        private ThreadViewModel GetLatestThread(int channelId)
        {
            var thread = channelService.GetLatestThread(channelId);

            if (thread != null)
            {
                return new ThreadViewModel
                {
                    AuthorName = thread.Author != null ? thread.Author.UserName : "",
                    Posted = thread.CreateAt.ToString(CultureInfo.InvariantCulture),
                    Title = thread.Title ?? ""
                };
            }

            return new ThreadViewModel();
        }

        private async Task<string> AddFile(IFormFile uploadedFile)
        {
            string path = "/Files/" + uploadedFile.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            Models.File file = new Models.File { Name = uploadedFile.FileName, Path = path };
            await fileService.UploadAsync(file);

            return path;
        }

        [HttpPost]
        public IActionResult Search(int id, string query)
        {
            return RedirectToAction("Topic", new { id, query });
        }
    }
}
