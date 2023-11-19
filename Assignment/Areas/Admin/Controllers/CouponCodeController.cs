using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponCodeController : Controller
    {
        IQL_CartDetails _QL_CartDetails;
        ICouponCodeServices _couponCodeServices;
        IUserServices _userServices;
        IQL_CouponCodeDetailsServices _QL_CouponCodeDetailsServices;
        IProductServices _productServices;
        ICouponCodeDetailsServices _couponCodeDetailsServices;
        IRoleServices _roleServices;
        string _Name;
        string _Avata;
        string _UserID;
        dynamic _CouponCodeID;
        public CouponCodeController()
        {
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _QL_CouponCodeDetailsServices = new QL_CouponCodeDetailsServices();
            _roleServices = new RoleServices();
            _couponCodeDetailsServices = new CouponCodeDetailsServices();
            _QL_CartDetails = new QL_CartDetails();
            _couponCodeServices = new CouponCodeServices();
        }
        public IActionResult ShowListCouponCode()
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
            var couponCode = _couponCodeServices.GetAllCouponCode().Where(c => c.EndDate > DateTime.Now && c.Status == 1);
            var couponCodeOverTime = _couponCodeServices.GetAllCouponCode().Where(c => c.EndDate <= DateTime.Now || c.Status != 1);
            if (couponCodeOverTime != null) // xóa mã giảm giá hết hạn hoặc status !=1
            {
                foreach (var item in couponCodeOverTime)
                {
                    _couponCodeServices.DeleteCouponCode(item.ID);
                }
            }
            return View(couponCode);
        }
        public IActionResult CreateCouponCode()
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
        public IActionResult CreateCouponCode(CouponCode CouponCode)
        {
            var listCouponCode = _couponCodeServices.GetAllCouponCode().FindAll(c => c.CouponCodes == CouponCode.CouponCodes);
            if (CouponCode.CouponValue != null || !string.IsNullOrWhiteSpace(CouponCode.CouponCodes))
            {
                if (CouponCode.EndDate >= DateTime.Now)
                {
                    if (listCouponCode.Count != 1)
                    {
                        _couponCodeServices.CreateCouponCode(CouponCode);
                        return RedirectToAction("ShowListCouponCode");
                    }
                    else
                    {
                        TempData["CreateCouponCode"] = "Coupon Code đã tồn tại";
                        return RedirectToAction("CreateCouponCode");
                    }
                }
                else
                {
                    TempData["CreateCouponCode"] = "Endtime không hợp lệ";
                    return RedirectToAction("CreateCouponCode");
                }
            }
            else
            {
                TempData["CreateCouponCode"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("CreateCouponCode");
            }
        }
        [HttpGet]
        public IActionResult UpdateCouponCode(Guid id) // Mở form, truyền luôn sang form
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
            var CouponCode = _couponCodeServices.GetAllCouponCode().FirstOrDefault(c => c.ID == id);
            return View(CouponCode);
        }
        public IActionResult UpdateCouponCode(CouponCode CouponCode)
        {
            _couponCodeServices.UpdateCouponCode(CouponCode);
            return RedirectToAction("ShowListCouponCode");
        }
        public IActionResult DeleteCouponCode(Guid id)
        {
            if (_couponCodeServices.DeleteCouponCode(id))
            {
                return RedirectToAction("ShowListCouponCode");
            }
            else return BadRequest();
        }
        public IActionResult ShowListCouponCodeDetails(Guid id)
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            
            HttpContext.Session.SetString("CouponCodeID", id.ToString());
            
            if (_Name != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
            }
            else
            {
                ViewBag.ProductInCart = 0;
            }
            var CouponCodeDetails = _QL_CouponCodeDetailsServices.GetAllCouponCode().FindAll(c => c.CouponCodeID == id);
            return View(CouponCodeDetails);
        }
        [HttpGet]
        public IActionResult ShowListProductForAddCoupon(string strSearch)
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
            _CouponCodeID = HttpContext.Session.GetString("CouponCodeID");
            var CouponCodeDetails = _QL_CouponCodeDetailsServices.GetAllCouponCode().FindAll(c => c.CouponCodeID == Guid.Parse(_CouponCodeID));
            var products = _productServices.GetAllProduct().Where(c => !CouponCodeDetails.Any(b => b.ProductID == c.ID));
            if (!string.IsNullOrEmpty(strSearch))
            {
                products = products.Where(c => c.Name.ToLower().Contains(strSearch.ToLower()) || c.Supplier.ToLower().Contains(strSearch.ToLower())).ToList();
            }
            return View(products); // Truyền tới View 1 object model
        }
        public IActionResult AddCouponCodeToProduct(Guid id)
        {
            _CouponCodeID = HttpContext.Session.GetString("CouponCodeID");
            var getCouponCode = _couponCodeServices.GetAllCouponCode().FirstOrDefault(c => c.ID == Guid.Parse(_CouponCodeID));
            var CouponCodeDetails = new CouponCodeDetails()
            {
                CouponCodeID = getCouponCode.ID,
                ProductID = id,
                CouponValue = getCouponCode.CouponValue
            };
            _couponCodeDetailsServices.CreateCouponCodeDetails(CouponCodeDetails);
            return Redirect("/Admin/CouponCode/ShowListCouponCodeDetails/" + _CouponCodeID);
        }
        public IActionResult DeleteProductInCouponCodeDetails(Guid id)
        {
            _QL_CouponCodeDetailsServices.DeleteCouponCodeDetails(id);
            return Redirect("/Admin/CouponCode/ShowListCouponCodeDetails/" + _CouponCodeID);
        }
    }
}
