using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class FileService : IFile
    {
        private readonly ApplicationDbContext db;
        public FileService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task UploadAsync(File file)
        {
            db.Add(file);
            await db.SaveChangesAsync();
        }
    }
}
