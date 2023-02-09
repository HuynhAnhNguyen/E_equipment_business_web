using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class CartController : Controller
    {
        readonly Service _service;
        public CartController(Service service)
        {
            this._service = service;
        }

        [Area("Store"), HttpGet]
        public IActionResult MyItems()
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            if(maKH == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Loai = _service.danhSachLoaiSP().ToList();
                ViewData["path"] = "/images/product/";
                ViewData["cart_items"] = _service.danhSachDonDat(0, maKH).ToList();
                return View(_service.danhSachDonDat(0, maKH).ToList());
            }
        }

        [Area("Store"), HttpPost]
        public PartialViewResult RemoveItems(string maDD)
        {
            var model = _service.getDonDat(maDD);
            if (model.TrangThai == 0)
            {
                _service.xoaDonDat(maDD);
            }
            return get_cart();

        }

        [Area("Store"), HttpPost]
        public PartialViewResult RemoveCartItem(string maDD)
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            var model = _service.getDonDat(maDD);
            if (model.TrangThai == 0)
            {
                _service.xoaDonDat(maDD);
            }

            ViewData["path"] = "/images/product/";
            return PartialView("_Cart_Full", _service.danhSachDonDat(0, maKH).ToList());
        }

        [Area("Store"), HttpPost]
        public JsonResult AddItems(string masp)
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            if (maKH == null)
            {
                return Json(null);
            }
            string kq = "Not add";
            if (!string.IsNullOrEmpty(masp))
            {
                kq = _service.themDonDat(masp, maKH);
            }
            return Json(kq);
        }

        [Area("Store"), HttpPost]
        public PartialViewResult get_cart()
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            return PartialView("_Cart", _service.danhSachDonDat(0, maKH).ToList());
        }

        [Area("Store"), HttpPost]
        public JsonResult Increase(string maDD, int soLuong)
        {
            _service.increase(maDD, soLuong);
            return Json(maDD + " " + soLuong);
        }
    }
}
