using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TN408.Models;

namespace TN408.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly Service _services;
        private TN408Context db = new TN408Context();

        public SanPhamController(Service services)
        {
            _services = services;
        }

        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                ViewData["path"] = "/images/product/";
                return View(_services.danhSachSanPham().ToList());
            }
            return RedirectToAction("Index", "Login");

        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsSanPham(string id)
        {
            ViewData["path"] = "/images/product/";
            var sanpham = db.SanPhams.Where(x => x.MaLoaiSp == id).Include(x => x.MaLoaiSpNavigation).Include(x => x.MaDvtNavigation).FirstOrDefault();
            return View(_services.getSanPham(id));
        }

        [Area("Admin"), HttpGet]
        public IActionResult DeleteSanPham(string id)
        {
            _services.xoaSanPham(id);
            return RedirectToAction("Index", "SanPham");
        }

        [Area("Admin"), HttpGet]
        public IActionResult CreateSanPham()
        {
            ViewData["Loais"] = _services.danhSachLoaiSP().ToList();
            ViewData["DonVis"] = _services.danhSachDVT().ToList();
            return View();
        }

        [Area("Admin"), HttpPost]
        public IActionResult CreateSanPham(SanPham sanPham, IFormFile file)
        {
            _services.themSanPham(sanPham, file);
            return RedirectToAction("Index", "SanPham");

        }

        [Area("Admin"), HttpGet]
        public IActionResult EditSanPham(string id)
        {
            ViewData["path"] = "/images/product/";
            var model = _services.getSanPham(id);
            ViewData["Loais"] = _services.danhSachLoaiSP().ToList();
            ViewData["DonVis"] = _services.danhSachDVT().ToList();
            return View(model);
        }
        [Area("Admin"), HttpPost]
        public IActionResult Edit(SanPham sanPham, IFormFile file)
        {
            _services.suaSanPham(sanPham, file);
            return RedirectToAction("Index", "SanPham");
        }
    }
}
