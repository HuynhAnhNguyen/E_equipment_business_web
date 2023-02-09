using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TN408.Areas.Store.Models;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class CheckoutController : Controller
    {
        readonly Service _service;

        public CheckoutController(Service service)
        {
            this._service = service;
        }
        [Area("Store"), HttpGet]
        public IActionResult Index()
        {
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
            return View(_service.danhSachDonDat(2).ToList().Take(2));
        }


        [Area("Store"), HttpGet]
        public IActionResult Confirm(string listdd) //"["4"]"
        {
            var st = JsonSerializer.Deserialize<List<string>>(listdd); //['4']
            var list = new List<DonDat>();

            if (st?.Count() > 0 && st.First().Contains("DD") )
            {
                foreach (var s in st)
                {
                    var dd = _service.getDonDat(s);
                    list.Add(dd);
                }
            }

            if (st?.Count() > 0 && !st.First().Contains("DD"))
            {
                string madd = _service.themDonDat(st.First());
                list.Add(_service.getDonDat(madd));
            }

            var makh = HttpContext.Session.GetString("CurrentUserID");

            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (makh != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, makh).ToList();
                HoaDonModel info = new HoaDonModel();
                var kh = _service.getKH(makh);
                info.Holot = kh.HoKhachHang;
                info.Ten = kh.TenKhachHang;
                info.SoDienThoai = kh.SoDienThoai;
                ViewData["info"] = info;
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
                ViewData["info"] = null;
            }
            return View(list);
        }

        [Area("Store"), HttpPost]
        public IActionResult Receipt(HoaDonModel model)
        {
            var st = JsonSerializer.Deserialize<List<string>>(model.DonDats);
            var list = new List<DonDat>();

            if (st?.Count() > 0 && st.First().Contains("DD"))
            {
                foreach (var s in st)
                {
                    var dd = _service.getDonDat(s);
                    if (dd.TrangThai != 1)
                        list.Add(dd);
                }
            }

            if (st?.Count() > 0 && !st.First().Contains("DD"))
            {
                string madd = _service.themDonDat(st.First());
                list.Add(_service.getDonDat(madd));
            }


            if (list.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            string new_mahd;
            if (makh != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, makh).ToList();
                new_mahd = _service.themHoaDon(makh);
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
                new_mahd = _service.themHoaDon();
            }
            foreach (var dd in list)
            {
                _service.themChiTietHD(new_mahd, dd.MaDonDat.ToString());
            }
            if (makh != null)
            {
                ViewData["cart_items"] = _service.danhSachDonDat(0, makh).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<DonDat>();
            }
            model.MaHoaDon = new_mahd;
            ViewData["info"] = model;
            return View(list);
        }
    }
}
