using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Xml.Linq;

namespace Assignment.Controllers
{
    public class CheckOut : Controller
    {
        string _Name;
        string _Avata;
        string _UserID;
        static int _CouponValue;
        static Guid _BillID;
        IQL_CartDetails _QL_CartDetails;
        IQL_BillDetailsServices _QL_BillDetailsServices;
        ICouponCodeServices _couponCodeServices;
        ICouponCodeDetailsServices _couponCodeDetailsServices;
        IUserServices _userServices;
        ICartDetailServices _cartDetailServices;
        IBillDetailServices _billDetailServices;
        IBillServices _billServices;
        IProductServices _productServices;
        public CheckOut()
        {
            _QL_CartDetails = new QL_CartDetails();
            _QL_BillDetailsServices = new QL_BillDetailsServices();
            _couponCodeServices = new CouponCodeServices();
            _couponCodeDetailsServices = new CouponCodeDetailsServices();
            _userServices = new UserServices();
            _cartDetailServices = new CartDetailServices();
            _billDetailServices = new BillDetailServices();
            _billServices = new BillServices();
            _productServices = new ProductServices();
        }
        public IActionResult CheckOuts()
        {
            _UserID =HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            string total =HttpContext.Session.GetString("Total");
            int CouponValue = ViewBag.CouponValue = _CouponValue;
            ViewBag.CartDetails = _QL_CartDetails.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            ViewBag.phivanchuyen = 30000;
            ViewBag.Total = total;
            ViewBag.TongTien = Convert.ToInt32(total) + ViewBag.phivanchuyen - Convert.ToInt32(CouponValue);
            if (total != null)
            {
                ViewBag.ProductInCart = _QL_CartDetails.GetAllCartDetail().Count(c => c.UserID == Guid.Parse(_UserID));
                var UserID = _UserID;
                var cartDetails = _QL_CartDetails.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
                return View();
            }
            TempData["errorCart"] = "Không có sản phẩm trong giỏ hàng";
            return RedirectToAction("ShowListCartDetails","Cart");
        }
        [HttpPost]
        public IActionResult CheckOuts(string RecipientAddress, string RecipientName, string RecipientPhoneNumber, string ShippingFee) //thanh toán
        {
            _UserID = HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            string total = HttpContext.Session.GetString("Total");
            var CouponValue = _CouponValue;
            ViewBag.CartDetails = _cartDetailServices.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
            if (CouponValue == null)
            {
                CouponValue = 0;
            }
            Bill bill = new Bill()
            {
                UserID = Guid.Parse(_UserID),
                CreatDate = DateTime.Now,
                Status = 1,
                Discount = CouponValue,
                TotalAmout = int.Parse(total) - CouponValue + int.Parse(ShippingFee),
                RecipientAddress = RecipientAddress,
                RecipientName = RecipientName,
                RecipientPhoneNumber = RecipientPhoneNumber,
                ShippingFee = int.Parse(ShippingFee)
            };
            _billServices.CreateBill(bill);
            _BillID = bill.ID;
            HttpContext.Session.SetString("BillID", bill.ID.ToString());
            
            var cartDetails = _cartDetailServices.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
            foreach (var item in cartDetails)
            {
                var hoadonct = new BillDetail()
                {
                    IDHD = bill.ID,
                    IDSP = item.IDSP,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Type = item.Type,
                    Image = item.Image,
                    Size = item.Size,
                    Color = item.Color
                };
                _billDetailServices.CreateBillDetail(hoadonct);
                _cartDetailServices.DeleteCartDetail(item.ID); // xóa trong cart
                var product = _productServices.GetAllProduct().FirstOrDefault(c => c.ID == item.IDSP);
                product.AvailableQuantity -= item.Quantity; //trừ sl tồn
                _productServices.UpdateProduct(product); // update lại
            }
            return RedirectToAction("PrintBill", "CheckOut");
        }
        [HttpPost]
        public IActionResult CheckCouponCode(string CouponCode) //check để thanh toán
        {
            _UserID =HttpContext.Session.GetString("UserID");
            _Avata = HttpContext.Session.GetString("Avata");
            _Name = HttpContext.Session.GetString("Name");
            string total = HttpContext.Session.GetString("Total");
            ViewBag.Avata = _Avata;
            ViewBag.User = _Name;
            ViewBag.Total = total;
            if (!string.IsNullOrEmpty(CouponCode))
            {
                ViewBag.CouponValue = 0;
                var couponCode = _couponCodeServices.GetAllCouponCode().FirstOrDefault(c => c.CouponCodes == CouponCode);
                var getCouponCodeID = couponCode != null ? couponCode.ID : Guid.Empty;
                var getProductInCart = _cartDetailServices.GetAllCartDetail().FindAll(c => c.UserID == Guid.Parse(_UserID));
                if (getCouponCodeID != Guid.Empty)
                {
                    var getListProductInCouponDetails = _couponCodeDetailsServices.GetAllCouponCode().FindAll(c => c.CouponCodeID == getCouponCodeID);
                    var validProductDetails = getListProductInCouponDetails
                            .Where(c => getProductInCart.Any(p => p.IDSP == c.ProductID));
                    if (validProductDetails.Any())
                    {
                        var CouponValue = _couponCodeServices.GetAllCouponCode().FirstOrDefault(c => c.CouponCodes == CouponCode).CouponValue;
                        HttpContext.Session.SetString("CouponValue",CouponValue.ToString());
                        _CouponValue = ViewBag.CouponValue = CouponValue;

                       return RedirectToAction("CheckOuts","CheckOut");
                    }
                    else
                    {
                        _CouponValue = 0;
                        ViewBag.CouponValue = 0;
                        return RedirectToAction("CheckOuts", "CheckOut");
                    }
                }
                else
                {
                    _CouponValue = 0;
                   ViewBag.CouponValue = 0;
                    return RedirectToAction("CheckOuts","CheckOut");
                }
            }
            else
            {
                _CouponValue = 0;
                ViewBag.CouponValue = 0;
                return RedirectToAction("CheckOuts", "CheckOut");
            }
            
        }
        public IActionResult PrintBill()
        {
           string billid =  HttpContext.Session.GetString("BillID");
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
            var billDetailsList = _QL_BillDetailsServices.GetAllBillDetail().FindAll(c => c.IDHD == Guid.Parse(billid));
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == Guid.Parse(billid));
            ViewBag.bill = bill;
            return View(billDetailsList);
        }
    }
}
