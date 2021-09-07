using ForumApp.Models;

namespace ForumApp.Interfaces
{
    public interface IComment
    {
        Comment GetById(int id);
    }
}
