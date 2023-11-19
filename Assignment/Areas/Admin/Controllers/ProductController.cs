using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IProductServices _productServices;
        IUserServices _userServices;
        IQL_CartDetails _QL_CartDetails;
        string _Name;
        string _Avata;
        string _UserID;
        public ProductController()
        {
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _QL_CartDetails = new QL_CartDetails();
        }
        [HttpGet]
        public IActionResult ShowListProduct(string strSearch)
        {
             _UserID = HttpContext.Session.GetString("UserID");
             _Avata = HttpContext.Session.GetString("Avata");
             _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name!=null && _UserID != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            var products = _productServices.GetAllProduct();
            if (!string.IsNullOrEmpty(strSearch))
            {
                products = products.Where(c => c.Name.ToLower().Contains(strSearch.ToLower()) || c.Supplier.ToLower().Contains(strSearch.ToLower())).ToList();
            }
            return View(products);
        }
        public IActionResult CreateProduct()
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
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            product.Status = 1;
            _productServices.CreateProduct(product);
            return RedirectToAction("ShowListProduct");

        }
        [HttpGet]
        public IActionResult UpdateProduct(Guid id) // Mở form, truyền luôn sang form
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
            var product = _productServices.GetAllProduct().FirstOrDefault(c => c.ID == id);
            return View(product);
        }
        public IActionResult UpdateProduct(Product product)
        {
            if (_productServices.UpdateProduct(product))
            {
                return RedirectToAction("ShowListProduct");
            }
            else return BadRequest();
        }
        public IActionResult DeleteProduct(Guid id)
        {
            if (_productServices.DeleteProduct(id))
            {
                return RedirectToAction("ShowListProduct");
            }
            else return BadRequest("Không xóa được");
        }
    }
}
