using Assignment.IServices;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
        
    public class HomeController : Controller
    {
        private IQL_BillServices _QL_BillServices;
        private IQL_BillDetailsServices qL_BillDetailsServices;
        IProductServices _productServices;


       
        public HomeController(IQL_BillServices QL_BillServices, IQL_BillDetailsServices qL_BillDetails, IProductServices productServices )
        {
            _QL_BillServices = QL_BillServices;
            qL_BillDetailsServices = qL_BillDetails;
           _productServices = productServices;
        }

        public IActionResult Index()
        {

            var bills = _QL_BillServices.GetBillsStatistics();

            var sp = _productServices.GetAllProduct().ToList();
            ViewBag.sp = sp;
            return View(bills);
        }
        [HttpPost]
       
        public JsonResult SearchByDate(DateTime startDate, DateTime endDate)
        {
          DateTime from = startDate.Date;
          DateTime to = endDate.Date;
            var sales = _QL_BillServices.GetBillsByDate(from, to);
            return Json(sales);
        }

    }
    }
