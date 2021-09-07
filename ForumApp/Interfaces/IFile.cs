using ForumApp.Models;
using System.Threading.Tasks;

namespace ForumApp.Interfaces
{
    public interface IFile
    {
        public Task UploadAsync(File file);
    }
}
