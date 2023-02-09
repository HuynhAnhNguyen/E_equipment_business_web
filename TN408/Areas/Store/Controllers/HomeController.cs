using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        readonly Service _service;

        public HomeController(Service service)
        {
            this._service = service;
        }

        [Area("Store")]
        public IActionResult Index()
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (maKH != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, maKH).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
            }

            ViewData["hot-items"] = _service.danhSachSanPham().ToList();
            return View(_service.danhSachSanPham().ToList());
        }

        [Area("Store"), HttpGet]
        public IActionResult getListCartItem()
        {
            return View(_service.danhSachDonDat().ToList());
        }
    }
}
