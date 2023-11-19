using Assignment.Models;

namespace Assignment.IServices
{
    public interface IPostService
    {
        public bool CreatePost(Post p);
        public bool UpdatePost(Post p);
        public bool DeletePost(Guid id);
        public Post Detail(Guid id);
        public List<Post> GetAll();
    }
}
