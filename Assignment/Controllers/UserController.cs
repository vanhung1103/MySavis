using Assignment.IServices;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace Assignment.Controllers
{
    public class UserController : Controller
    {
        public IUserServices _iuserServices;
        public UserController(IUserServices userServices) {
                _iuserServices = userServices;
          
        } 
        public IActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Update()
        {
            string _UserID = HttpContext.Session.GetString("UserID");
            Guid id = Guid.Parse(_UserID);

            var user = _iuserServices.GetAllUser().FirstOrDefault(c => c.UserID == id);

            return View(user);
        }

        public IActionResult Update(User user)
        {
            _iuserServices.UpdateUser(user);
            return View();
        }

    }
}
