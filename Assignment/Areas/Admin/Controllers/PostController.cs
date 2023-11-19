using Assignment.IServices;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PostController : Controller
    {
        public IPostService postService;
        public PostController(IPostService post) { 
            postService = post;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post p)
        {
            p.Status = 1;
            postService.CreatePost(p);
            return RedirectToAction("Show");

        }
        public IActionResult Show()
        {
                        var posts = postService.GetAll();
            return View(posts);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var post = postService.GetAll().FirstOrDefault(c => c.Id == id);
            return View(post);
        }
        [HttpPut]
        public IActionResult Edit(Post p)
        {
            postService.UpdatePost(p);
            return RedirectToAction("Show");

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            postService.DeletePost(id);
            return RedirectToAction("Show");

        }
    }
}
