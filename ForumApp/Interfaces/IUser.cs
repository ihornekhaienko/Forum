using ForumApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApp.Interfaces
{
    public interface IUser
    {
        public Task Add(ApplicationUser user);
        public Task BanAsync(ApplicationUser user);
        public Task UnbanAsync(ApplicationUser user);
        public ApplicationUser GetByName(string name);
        public Task MakeAdminAsync(ApplicationUser user);
        public IEnumerable<ApplicationUser> GetAll();
        public ApplicationUser GetById(string id);
        public Task SetProfileImageAsync(string id, string link);
    }
}
