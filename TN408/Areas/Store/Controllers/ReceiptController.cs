using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class ReceiptController : Controller
    {
        readonly Service _service;
        public ReceiptController(Service service)
        {
            this._service = service;
        }
        [Area("Store"), HttpGet]
        public IActionResult List()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = _service.danhSachHoaDon(HttpContext.Session.GetString("CurrentUserID")).ToList();

            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (makh != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, makh).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
            }
            return View(model);
        }

        [Area("Store"), HttpGet]
        public IActionResult Detail(string mahd)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = _service.getHoaDon(mahd);

            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            ViewData["hoadon"] = _service.getHoaDon(mahd);
            ViewData["sum"] = _service.tongGiaTri(mahd);
            if (makh != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, makh).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
            }
            return View(model);
        }
    }
}
