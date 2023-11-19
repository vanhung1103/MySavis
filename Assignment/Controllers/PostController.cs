using Assignment.IServices;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class PostController : Controller
    {
        public IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show()
        {
            var posts = postService.GetAll();
            return View(posts);
        }
        public IActionResult Detail(Guid id)
        {
            var post = postService.Detail(id);
            return View(post);
        }
    }
}
