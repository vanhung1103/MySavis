using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Assignment.Controllers
{
    public class LoginController : Controller
    {
        IUserServices _userServices;
        IRoleServices _roleServices;
        ICartServices _cartServices;
        public LoginController()
        {
            _userServices = new UserServices();
            _roleServices = new RoleServices();
            _cartServices = new CartServices();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserID", "");
            HttpContext.Session.SetString("RoleName", "");
            HttpContext.Session.SetString("Avata", "");
            HttpContext.Session.SetString("Name", "");
            HttpContext.Session.SetString("UserName", "");
             return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public IActionResult Login(string username ,string password)
        {
            if (ModelState.IsValid)
            {
                var countUser = _userServices.GetAllUser().Count(c=>c.Username.ToLower() == username.ToLower()&&c.Password==password);
                if (countUser==1)
                {
                    var UserID = _userServices.GetAllUser().FirstOrDefault(c => c.Username.ToLower() == username.ToLower()).UserID;
                    var RoleID = _userServices.GetAllUser().FirstOrDefault(c => c.Username.ToLower() == username.ToLower()).RoleID;
                    var RoleName = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleID == RoleID).RoleName;
                    var Avata = _userServices.GetAllUser().FirstOrDefault(c => c.Username == username).Avata;
                    var Name = _userServices.GetAllUser().FirstOrDefault(c => c.Username == username).Name;
                    HttpContext.Session.SetString("UserID", UserID.ToString());
                    HttpContext.Session.SetString("RoleName", RoleName.ToString());
                    HttpContext.Session.SetString("Avata", Avata.ToString());
                    HttpContext.Session.SetString("Name", Name.ToString());
                    HttpContext.Session.SetString("UserName", username);
                    if (RoleName=="admin"|| RoleName == "staff")
                    {
                        return RedirectToAction("ShowListProduct", "Product",new { area = "Admin" });  
                    }
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return View();
                }
            }
            else
            { 
                return View();
            }
        }

        public IActionResult Login()
        { 
                return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(string username, string name, string numberphone)
        {
            if (ModelState.IsValid)
            {
                var user = _userServices.GetAllUser().FirstOrDefault(c => c.Username == username);
                if (user != null)
                {
                    if (user.Name == name && user.PhoneNumber == numberphone)
                    {
                        return RedirectToAction("ChangePassword","Login");
                    }
                    else
                    {
                        TempData["error"] = "Thông tin tài khoản không trùng khớp!";
                        return View();
                    };
                }
                else
                {
                    TempData["error"] = "Tài khoản không tồn tại!";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string username, string password, string oldpassword, string repassword)
        {
            if (ModelState.IsValid)
            {
                var User = _userServices.GetAllUser();
                int dem = User.Count(c => c.Username == username && c.Password == oldpassword);
                if (dem == 1)
                {
                    if (password == repassword)
                    {
                        var user = User.FirstOrDefault(c => c.Username == username);
                        user.Password = repassword;
                        _userServices.UpdateUser(user);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["error"] = "Password và rePassword không hợp lệ!";
                        return RedirectToAction("ChangePassword");
                    }
                }
                else
                {
                    TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác!";
                    return RedirectToAction("ChangePassword");
                }
            }
            else
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin!";
                return RedirectToAction("ChangePassword");
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {

            if (!string.IsNullOrEmpty(user.Name) || !string.IsNullOrEmpty(user.Username) || !string.IsNullOrEmpty(user.Password) || !string.IsNullOrEmpty(user.PhoneNumber))
            {
                if (IsExsisUser(user.Username))
                {
                    var getRoleID = _roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == "user").RoleID;
                    user.Status = 1;
                    user.Avata = "avata.png";
                    user.RoleID = getRoleID;
                    _userServices.CreateUser(user);
                    var cart = new Cart();
                    cart.UserID = _userServices.GetAllUser().FirstOrDefault(c => c.Username == user.Username).UserID;
                    cart.Description = "OK";
                    _cartServices.CreateCart(cart);
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = "Tài khoản đã tồn tại!";
                    return RedirectToAction("SignUp");
                }

            }
            else
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin!";
                return RedirectToAction("SignUp");
            }
        }
        public bool IsExsisUser(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                var isExsist = _userServices.GetAllUser().Count(c => c.Username == username);
                if (isExsist == 0)
                {

                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
