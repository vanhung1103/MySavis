using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserServices _userServices;
        ICartServices _cartServices;
        IRoleServices _roleServices;
        IQL_CartDetails _QL_CartDetails;
        IQL_UserServices _QL_UserServices;
        string _Name;
        string _Avata;
        string _UserID;
        public UserController()
        {
            _userServices = new UserServices();
            _cartServices = new CartServices();
            _roleServices = new RoleServices();
            _QL_CartDetails = new QL_CartDetails();
            _QL_UserServices = new QL_UserServices();
        }
        [HttpGet]
        public IActionResult ShowListUser(string strSearch)
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            string rolename = HttpContext.Session.GetString("RoleName");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (rolename!="admin")
            {
                return RedirectToAction("ThongBao", "ThongBao");
            }
            else
            {
                if (_Name != null)
                {
                    ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
                }
                else
                {
                    ViewBag.ProductInCart = 0;
                }
                var userList = _QL_UserServices.GetAllUser()/*.Where(c => c.RoleName != "user")*/;
                if (!string.IsNullOrEmpty(strSearch))
                {
                    userList = userList.Where(c => c.Username.ToLower().Contains(strSearch.ToLower()) || c.RoleName.ToLower().Contains(strSearch.ToLower()) || c.PhoneNumber == strSearch).ToList();
                }

                return View(userList);
            }
        }
        public IActionResult CreateUser()
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            ViewBag.AllRole = _roleServices.GetAllRole();
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user, string rolename)
        {
            user.RoleID = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == rolename).RoleID;
            _userServices.CreateUser(user);
            var cart = new Cart();
            cart.UserID = _userServices.GetAllUser().FirstOrDefault(c => c.Username == user.Username).UserID;
            cart.Description = "OK";
            _cartServices.CreateCart(cart);
            return RedirectToAction("ShowListUser");

        }
        public IActionResult UpdateUser(User user, string rolename)
        {
            user.RoleID = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == rolename).RoleID;
            if (_userServices.UpdateUser(user))
            {
                return RedirectToAction("ShowListUser");
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult UpdateUser(Guid id) // Mở form, truyền luôn sang form
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            ViewBag.AllRole = _roleServices.GetAllRole();
            var user = _QL_UserServices.GetAllUser().FirstOrDefault(c => c.UserID == id);
            return View(user);
        }
        public IActionResult DeleteUser(Guid id)
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            if (_userServices.DeleteUser(id))
            {
                _cartServices.DeleteCart(id);
                var CartID = _QL_CartDetails.GetAllCartDetail().FirstOrDefault(c => c.UserID == id).ID;
                _QL_CartDetails.DeleteCartDetail(CartID);
                return RedirectToAction("ShowListUser");
            }
            else return BadRequest();
        }
    }
}
