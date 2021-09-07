using Microsoft.AspNetCore.Http;
using System;

namespace ForumApp.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ProfileImageLink { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime CreateAt { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
