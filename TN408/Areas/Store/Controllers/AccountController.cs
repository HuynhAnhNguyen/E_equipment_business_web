using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class AccountController : Controller
    {
        readonly Service _service;
        public AccountController(Service service)
        {
            this._service = service;
        }
        [Area("Store"), HttpGet]
        public IActionResult Info()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = _service.getKH(HttpContext.Session.GetString("CurrentUserID"));
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
            return View(model);
        }
    }
}
