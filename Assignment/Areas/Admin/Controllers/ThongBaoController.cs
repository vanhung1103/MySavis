using Microsoft.AspNetCore.Mvc;

namespace Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongBaoController : Controller
    {
        public IActionResult ThongBao()
        {
            return View();
        }
    }
}
