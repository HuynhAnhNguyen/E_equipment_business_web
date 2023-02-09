using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Service _service;

        public KhachHangController(Service service)
        {
            _service = service;
        }
        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                return View(_service.danhSachKH());
            }
            return RedirectToAction("Index", "Login"); ;
        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsKhachHang(string id)
        {
            return View(_service.getKH(id));
        }
    }
}
