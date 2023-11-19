using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class PostService : IPostService
    {
        DbContexts context;
        public PostService()
        {
            context = new DbContexts();
        }
        public bool CreatePost(Post p)
        {
            try
            {
                context.posts.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
       
        public bool DeletePost(Guid id)
        {
            try
            {
                var Product = context.posts.Find(id);
                context.Remove(Product);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Post> GetAll()
        {

            return context.posts.ToList();
        }

        public bool UpdatePost(Post p)
        {
            try
            {
                var post = context.posts.Find(p.Id);
                post.Id = p.Id;
                post.UserId = p.UserId;
                post.Contents = p.Contents;
                post.Tittle = p.Tittle;
                post.TittleImage = p.TittleImage;
                post.CreateAt = p.CreateAt;
                post.UpdateAt = p.UpdateAt;
                post.Description = p.Description;
                post.Status = p.Status;
                context.posts.Update(post);
                context.SaveChanges();
              
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public Post Detail(Guid id)
        {
            try
            {
                var post = context.posts.FirstOrDefault(c => c.Id == id);
                return post;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
