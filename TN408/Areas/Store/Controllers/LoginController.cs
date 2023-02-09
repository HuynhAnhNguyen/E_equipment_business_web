using Microsoft.AspNetCore.Mvc;
using TN408.Areas.Store.Models;
using TN408.Models;

namespace TN408.Areas.Store.Controllers
{
    public class LoginController : Controller
    {
        readonly Service _service;
        public LoginController(Service service)
        {
            this._service = service;
        }
        [Area("Store"), HttpGet]
        public ActionResult Index()
        {
            var session = HttpContext.Session;
            string user = session.GetString("CurrentUser");
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginModel model = new LoginModel();
            if (HttpContext.Request.Cookies.ContainsKey("sdt"))
            {
                model.SoDienThoai = HttpContext.Request.Cookies["sdt"];
            }
            return View(model);
        }

        [Area("Store"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                KhachHang search = _service.loginKH(model.SoDienThoai, model.Password);
                if (search == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    return View(model);
                }
                if (model.Rememberme)
                {
                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(3);
                    HttpContext.Response.Cookies.Append("sdt", model.SoDienThoai, opt);
                }
                else
                {
                    if (HttpContext.Request.Cookies.ContainsKey("sdt"))
                    {
                        CookieOptions opt = new CookieOptions();
                        opt.Expires = DateTime.Now.AddDays(-1);
                        HttpContext.Response.Cookies.Append("sdt", model.SoDienThoai, opt);
                    }
                }
                var session = HttpContext.Session;
                session.SetString("CurrentUser", search.HoKhachHang + " " + search.TenKhachHang);
                session.SetString("CurrentUserID", search.MaKhachHang.ToString());
                Console.WriteLine(search.MaKhachHang.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Area("Store"), HttpGet]
        public ActionResult Logout()
        {
            var session = HttpContext.Session;
            session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Area("Store"), HttpGet]
        public IActionResult Register()
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
            return View();
        }

        [Area("Store"), HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MatKhau == model.MatKhau2)
                {
                    _service.themKH(model);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Xác nhận mật khẩu không trùng khớp");
                    return View(model);
                }
            }
            return View(model);
        }


    }
}
