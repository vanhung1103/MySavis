using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Assignment.Controllers
{
    public class CartController : Controller
    {
        string _Name;
        string _Avata;
        string _UserID;
        IUserServices _userServices;
        IQL_CartDetails _QL_CartDetails;
        ICartDetailServices _cartDetailServices;
        IProductServices _productServices;
        public CartController()
        {
            _userServices = new UserServices();
            _QL_CartDetails = new QL_CartDetails();
            _cartDetailServices = new CartDetailServices();
            _productServices = new ProductServices();
        }
        bool IsExsistProductInCart(Guid idsp, Guid userid)
        {
            var Exsist = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c => c.IDSP == idsp && c.UserID == userid);
            if (Exsist != null)
            {
                return true;
            }
            return false;
        }
        public IActionResult ShowListCartDetails()
        {

            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID ==Guid.Parse(_UserID));
                var cartDetails = _QL_CartDetails.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
                int total = 0;
                foreach (var item in cartDetails)
                {
                    total += (item.Quantity * item.Price);
                }
                ViewBag.Total = total;
                HttpContext.Session.SetString("Total", total.ToString());
                //lấy tất cả sản phẩm ở list cartdetails (lấy dc all idsp) và check ở list product, nếu bất kì sản phẩm nào có số lượng <1 thì thông báo
                var checklst = cartDetails.Where(p => _productServices.GetAllProduct().Any(c => c.ID == p.ProductID && c.AvailableQuantity < 1));
                if (checklst.Any())
                {
                    foreach (var item in checklst)
                    {
                        _cartDetailServices.DeleteCartDetail(item.ProductID);
                        TempData["deleteProductInCart"] = "1 số sản phẩm bị xóa do hết hàng";
                    }
                    cartDetails = _QL_CartDetails.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
                    return View(cartDetails);
                }
                return View(cartDetails);
            }
            return RedirectToAction("Login", "Login");

        }
        [HttpPost]
        public IActionResult AddProductToCart(Guid id, int quantityDetails)
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
            if (quantityDetails == null)
            {
                quantityDetails = 1;
            }
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
                if (IsExsistProductInCart(id, Guid.Parse(_UserID)))
                {
                    var cart = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c => c.IDSP == id && c.UserID == Guid.Parse(_UserID));
                    cart.Quantity += quantityDetails;
                    _cartDetailServices.UpdateCartDetail(cart);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var Product = _productServices.GetAllProduct().FirstOrDefault(c => c.ID == id);
                    var cartDetail = new CartDetail();
                    cartDetail.IDSP = Product.ID;
                    cartDetail.UserID = Guid.Parse(_UserID);
                    cartDetail.Quantity = quantityDetails;
                    cartDetail.Price = Product.Price;
                    cartDetail.Size = Product.Size;
                    cartDetail.Color = Product.Color;
                    cartDetail.Type = Product.Type;
                    cartDetail.Image = Product.Image;
                    _cartDetailServices.CreateCartDetail(cartDetail);
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }
        public IActionResult AddProductToCart(Guid id)
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
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
                if (IsExsistProductInCart(id, Guid.Parse(_UserID)))
                {
                    var cart = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c => c.IDSP == id && c.UserID == Guid.Parse(_UserID));
                    cart.Quantity++;
                    _cartDetailServices.UpdateCartDetail(cart);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    var Product = _productServices.GetAllProduct().FirstOrDefault(c => c.ID == id);
                    var cartDetail = new CartDetail();
                    cartDetail.IDSP = Product.ID;
                    cartDetail.UserID = Guid.Parse(_UserID);
                    cartDetail.Quantity = 1;
                    cartDetail.Price = Product.Price;
                    cartDetail.Size = Product.Size;
                    cartDetail.Color = Product.Color;
                    cartDetail.Type = Product.Type;
                    cartDetail.Image = Product.Image;
                    _cartDetailServices.CreateCartDetail(cartDetail);
                    return RedirectToAction("Index","Home");
                    
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public IActionResult DeleteProductInCart(Guid id)
        {
            if (_cartDetailServices.DeleteCartDetail(id))
            {
                return RedirectToAction("ShowListCartDetails");
            }
            else return BadRequest();
        }
        public IActionResult ClearCart()
        {
            _UserID = HttpContext.Session.GetString("UserID");
            ViewBag.User = _Name;
            var GetCartDetails = _cartDetailServices.GetAllCartDetail().Where(c => c.UserID == Guid.Parse(_UserID)).ToList();
            foreach (var item in GetCartDetails)
            {
                _cartDetailServices.DeleteCartDetail(item.ID);
            }
            TempData["errorCart"] = "Không có sản phẩm trong giỏ hàng";
            return RedirectToAction("ShowListCartDetails");
        }
        public IActionResult PlusProductInCart(Guid id)
        {
            _UserID = HttpContext.Session.GetString("UserID");
            var cart = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c => c.ID == id && c.UserID == Guid.Parse(_UserID));
            cart.Quantity++;
            _cartDetailServices.UpdateCartDetail(cart);
            return RedirectToAction("ShowListCartDetails");
        }
        public IActionResult MinusProductInCart(Guid id)
        {
            _UserID = HttpContext.Session.GetString("UserID");
            var cart = _cartDetailServices.GetAllCartDetail().FirstOrDefault(c => c.ID == id && c.UserID == Guid.Parse(_UserID));
            if (cart.Quantity == 1)
            {
                TempData["min"] = "Số lượng đang là tối thiểu";
                TempData["id"] = id;
                return RedirectToAction("ShowListCartDetails");
            }
            else
            {
                cart.Quantity--;
                _cartDetailServices.UpdateCartDetail(cart);
                TempData["min"] = "";
                return RedirectToAction("ShowListCartDetails");

            }
        }
    }
}
