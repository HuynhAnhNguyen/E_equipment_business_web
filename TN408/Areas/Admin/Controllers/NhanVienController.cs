using Microsoft.AspNetCore.Mvc;
using TN408.Models;

namespace TN408.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly Service _service;

        public NhanVienController(Service service)
        {
            _service = service;
        }

        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                return View(_service.danhSachNhanVien());
            }
            return RedirectToAction("Index", "Login"); ;
        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsNhanVien(string id)
        {
            return View(_service.getNhanVien(id));
        }

        [Area("Admin"), HttpGet]
        public IActionResult CreateNhanVien()
        {
            ViewData["Nhoms"] = _service.danhsachNhom().ToList();
            return View();
        }

        [Area("Admin"), HttpPost]
        public IActionResult CreateNhanVien(NhanVien nV)
        {
            ViewData["Nhoms"] = _service.danhsachNhom().ToList();
            if (ModelState.IsValid)
            {
                _service.themNV(nV.HoNhanVien, nV.TenNhanVien, nV.NgaySinh, nV.GioiTinh, nV.SoDienThoai, nV.DiaChi, nV.MatKhau, "3");
                return RedirectToAction("Index", "NhanVien");
            }
            return View(nV);

        }

        [Area("Admin"), HttpGet]
        public IActionResult DeleteNhanVien(string id)
        {
            _service.xoaNhanVien(id);
            return RedirectToAction("Index", "NhanVien");
        }


        [Area("Admin"), HttpGet]
        public IActionResult EditNhanVien(string id)
        {
            ViewData["Nhoms"] = _service.danhsachNhom().ToList();
            var model = _service.getNhanVien(id);
            return View(model);
        }

        [Area("Admin"), HttpPost]
        public IActionResult EditNhanVien(NhanVien nV)
        {
            ViewData["Nhoms"] = _service.danhsachNhom().ToList();
            _service.suaNhanVien(nV);
            return RedirectToAction("Index", "NhanVien");

        }
    }
}
