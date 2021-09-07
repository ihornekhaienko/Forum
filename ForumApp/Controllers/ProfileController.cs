using ForumApp.Interfaces;
using ForumApp.Models;
using ForumApp.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment appEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUser userService;
        private readonly IFile fileService;

        public ProfileController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, IUser userService, IFile fileService)
        {
            this.appEnvironment = appEnvironment;
            this.userManager = userManager;
            this.userService = userService;
            this.fileService = fileService;
        }

        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            var user = userService.GetById(id);

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new ProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfileImageLink = user.ProfileImageUrl,
                CreateAt = user.RegisterAt,
                IsActive = user.IsActive,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = userManager.GetUserId(User);
            string path = await AddFile(file);
            await userService.SetProfileImageAsync(userId, path);

            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
        private async Task<string> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                return "/images/avatar.png";

            string path = "/Files/" + uploadedFile.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            Models.File file = new Models.File { Name = uploadedFile.FileName, Path = path };
            await fileService.UploadAsync(file);

            return path;
        }

        public IActionResult Index()
        {
            var profiles = userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileViewModel
                {
                    UserId = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    ProfileImageLink = u.ProfileImageUrl,
                    CreateAt = u.RegisterAt,
                    IsActive = u.IsActive
                });

            var model = new ProfileListModel
            {
                Profiles = profiles
            };

            return View(model);
        }

        public async Task<IActionResult> Ban(string id)
        {
            var user = userService.GetById(id);
            await userService.BanAsync(user);
            return RedirectToAction("Index", "Profile");
        }
        public async Task<IActionResult> Unban(string id)
        {
            var user = userService.GetById(id);
            await userService.UnbanAsync(user);
            return RedirectToAction("Index", "Profile");
        }
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = userService.GetById(id);
            await userService.MakeAdminAsync(user);
            return RedirectToAction("Index", "Profile");
        }
    }
}
