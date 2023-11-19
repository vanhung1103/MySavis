using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        string _Name;
        string _Avata;
        string _UserID;
        IProductServices _productServices;
        IQL_CartDetails _QL_CartDetails;
        public HomeController()
        {
            _productServices = new ProductServices();
            _QL_CartDetails = new QL_CartDetails();
        }
        public IActionResult Index()
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name!=null )
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            var PriceDesc = _productServices.GetAllProduct().Where(c => c.AvailableQuantity >= 1 && c.Status == 1).OrderByDescending(c => c.Price).Take(4).ToList();
            var PriceAsc = _productServices.GetAllProduct().Where(c => c.AvailableQuantity >= 1 && c.Status == 1).OrderBy(c => c.Price).Take(4).ToList();
            var Nike = _productServices.GetAllProduct().Where(c => c.AvailableQuantity >= 1 && c.Status == 1 && c.Supplier == "nike").OrderBy(c => c.Price).Take(4).ToList();
            var Mlb = _productServices.GetAllProduct().Where(c => c.AvailableQuantity >= 1 && c.Status == 1 && c.Supplier == "mlb").OrderBy(c => c.Price).Take(4).ToList();
            var Adidas = _productServices.GetAllProduct().Where(c => c.AvailableQuantity >= 1 && c.Status == 1 && c.Supplier == "adidas").OrderBy(c => c.Price).Take(4).ToList();
            ViewBag.PriceDesc = PriceDesc;
            ViewBag.PriceAsc = PriceAsc;
            ViewBag.Nike = Nike;
            ViewBag.Mlb = Mlb;
            ViewBag.Adidas = Adidas;
            return View();
        }
        public IActionResult Introduct()
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
        [HttpGet]
        public IActionResult ShowAllProduct(string Supplier, string strSearch, string Price)
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
            if (Price != null)
            {
                if (Price == "++")
                {
                    var listProducts = _productServices.GetAllProduct().FindAll(c => c.Status == 1 && c.AvailableQuantity >= 1).OrderBy(c => c.Price).ToList();
                    return View(listProducts);
                }
                else
                {
                    var listProducts = _productServices.GetAllProduct().FindAll(c => c.Status == 1 && c.AvailableQuantity >= 1).OrderByDescending(c => c.Price).ToList();
                    return View(listProducts);
                }
            }
            if (Supplier != null)
            {
                var listProducts = _productServices.GetAllProduct().FindAll(c => c.Status == 1 && c.AvailableQuantity >= 1 && c.Supplier == Supplier).ToList();
                if (!listProducts.Any())
                {
                    ViewBag.CountError = "Không tìm thấy kết quả!";
                }
                return View(listProducts);
            }
            if (strSearch != null)
            {
                var products = _productServices.GetAllProduct().Where(c => c.Name.ToLower().Contains(strSearch.ToLower()) || c.Supplier.ToLower().Contains(strSearch.ToLower())).ToList();
                if (!products.Any())
                {
                    ViewBag.CountError = "Không tìm thấy kết quả!";
                }
                return View(products);
            }
            else
            {
                var listProduct = _productServices.GetAllProduct().FindAll(c => c.Status == 1 && c.AvailableQuantity >= 1).ToList();
                return View(listProduct);
            }

        }
        [HttpGet]
        public IActionResult DetailsProduct(Guid id) // Mở form, truyền luôn sang form
        {
            var product = _productServices.GetAllProduct().FirstOrDefault(c => c.ID == id);
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
            return View(product);
        }
    }
}
