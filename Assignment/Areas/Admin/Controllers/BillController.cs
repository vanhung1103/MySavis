using Assignment.IServices;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        string _Name;
        string _Avata;
        string _UserID;
        IQL_BillServices _QL_BillServices;
        IBillServices _billServices;
        IQL_BillDetailsServices _QL_BillDetailsServices;
        IQL_CartDetails _QL_CartDetails;
        IBillDetailServices _billDetailServices;
        IProductServices _productServices;
        public BillController()
        {
            _QL_BillServices = new QL_BillServices();
            _QL_BillDetailsServices = new QL_BillDetailsServices();
            _QL_CartDetails = new QL_CartDetails();
            _billServices = new BillServices();
            _billDetailServices = new BillDetailServices();
            _productServices = new ProductServices();
        }
        [HttpGet]
        public IActionResult ShowListBill(string strSearch,string Status)
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
            var billList = _QL_BillServices.GetAllBill().OrderByDescending(c => c.CreatDate);
            if (!string.IsNullOrEmpty(strSearch))
            {
                var billLists = _QL_BillServices.GetAllBill();
                billLists = billLists.Where(c => c.UserName.ToLower().Contains(strSearch.ToLower()) || c.RecipientName.ToLower().Contains(strSearch.ToLower()) || c.RecipientPhoneNumber == strSearch || c.CreatDate.ToString("dd/MM/yyyy").Contains(strSearch.ToLower())).OrderByDescending(c => c.CreatDate).ToList();
                return View(billLists);
            }
            if (Status!=null)
            {
                var billListStatus = _QL_BillServices.GetAllBill().Where(c=>c.Status==int.Parse(Status)).OrderByDescending(c => c.CreatDate);
                return View(billListStatus);
            }
            return View(billList);
        }
        public IActionResult ShowListBillDetails(Guid id)
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
            var billDetailsList = _QL_BillDetailsServices.GetAllBillDetail().FindAll(c => c.IDHD == id);
            return View(billDetailsList);
        }
        [HttpGet]
        public IActionResult UpdateStatusBill2(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 2;
            _billServices.UpdateBill(bill);
            return RedirectToAction("ShowListBill");
        }
        [HttpGet]
        public IActionResult UpdateStatusBill3(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 3;
            _billServices.UpdateBill(bill);
            return RedirectToAction("ShowListBill");
        }
        [HttpGet]
        public IActionResult UpdateStatusBill4(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 4;
            _billServices.UpdateBill(bill);
            return RedirectToAction("ShowListBill");
        }
        [HttpGet]
        public IActionResult UpdateStatusBill5(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 5;
            _billServices.UpdateBill(bill);
            return RedirectToAction("ShowListBill");
        }
        [HttpGet]
        public IActionResult UpdateStatusBill6(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 6;
            _billServices.UpdateBill(bill);
            var getListBillDetails = _billDetailServices.GetAllBillDetail().FindAll(c => c.IDHD == id);
            foreach (var billdetails in getListBillDetails)
            {
                foreach (var products in _productServices.GetAllProduct())
                {
                    if (products.ID == billdetails.IDSP)
                    {
                        products.AvailableQuantity += billdetails.Quantity;
                        _productServices.UpdateProduct(products);
                    }
                }
            }
            return RedirectToAction("ShowListBill");
        }
        [HttpGet]
        public IActionResult UpdateStatusBill0(Guid id)
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
            var bill = _billServices.GetAllBill().FirstOrDefault(c => c.ID == id);
            bill.Status = 0;
            _billServices.UpdateBill(bill);
            var getListBillDetails = _billDetailServices.GetAllBillDetail().FindAll(c => c.IDHD == id);
            foreach (var billdetails in getListBillDetails)
            {
                foreach (var products in _productServices.GetAllProduct())
                {
                    if (products.ID == billdetails.IDSP)
                    {
                        products.AvailableQuantity += billdetails.Quantity;
                        _productServices.UpdateProduct(products);
                    }
                }
            }
            return RedirectToAction("ShowListBill");
        }
    }
}
