using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task Add(ApplicationUser user)
        {
            db.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task BanAsync(ApplicationUser user)
        {
            user.IsActive = false;
            db.Update(user);
            await db.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return db.ApplicationUsers.FirstOrDefault(user => user.Id == id);
        }

        public ApplicationUser GetByName(string name)
        {
            return db.ApplicationUsers.FirstOrDefault(user => user.UserName == name);
        }

        public async Task MakeAdminAsync(ApplicationUser user)
        {
            await userManager.AddToRoleAsync(user, "Admin");
            db.Update(user);
            await db.SaveChangesAsync();
        }

        public async Task SetProfileImageAsync(string id, string link)
        {
            var user = GetById(id);
            await userManager.AddToRoleAsync(user, "Admin");
            await db.SaveChangesAsync();
        }

        public async Task UnbanAsync(ApplicationUser user)
        {
            user.IsActive = true;
            db.Update(user);
            await db.SaveChangesAsync();
        }
    }
}
