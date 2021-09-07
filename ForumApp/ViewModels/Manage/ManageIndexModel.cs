using System.ComponentModel.DataAnnotations;

namespace ForumApp.ViewModels.Manage
{
    public class ManageIndexModel
    {
        public string UserName { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        public string StatusMessage { get; set; }
    }
}
