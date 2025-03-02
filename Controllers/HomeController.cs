using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webbanson.Data;
using Webbanson.Models;

namespace Webbanson.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ComesticsContext db;

        // Constructor duy nhất nhận cả ILogger và ComesticsContext
        public HomeController(ILogger<HomeController> logger, ComesticsContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Trangchu()
        {
            return View();
        }

        public IActionResult Sanpham(int? Loai)
        {
            var sanpham = db.Products.AsQueryable();

            if (Loai.HasValue)
            {
                sanpham = sanpham.Where(p => p.CategoryId == Loai.Value.ToString());
            }

            var result = sanpham.Select(p => new
            {
                MaHh = p.ProductId,
                TenHH = p.ProductName,
                GiaGoc = p.OriginalPrice,
                GiaGiam = p.DiscountedPrice,
                Hinh = p.ImageUrl ?? "",
                MoTa = p.Description ?? "",
                TenLoai = p.Category.CategoryName
            }).ToList();

            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var hangHoas = db.Products.AsQueryable();

            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.ProductName.Contains(query));
            }

            var result = hangHoas.Select(p => new
            {
                MaHh = p.ProductId,
                TenHH = p.ProductName,
                GiaGoc = p.OriginalPrice,
                GiaGiam = p.DiscountedPrice,
                Hinh = p.ImageUrl ?? "",
                MoTa = p.Description ?? "",
                TenLoai = p.Category.CategoryName
            });

            return View(result);
        }

        public IActionResult Giohang()
        {
            return View();
        }

        public IActionResult Dangnhap()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XulyDangKy(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu tên đăng nhập đã tồn tại
                var existingCustomer = db.Customers
                    .FirstOrDefault(c => c.Username == customer.Username);

                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View("Dangky");
                }

                // Thêm khách hàng vào cơ sở dữ liệu
                db.Customers.Add(customer);
                db.SaveChanges();

                // Redirect tới trang đăng nhập hoặc trang chủ
                return RedirectToAction("DangNhap", "Account");
            }

            // Nếu có lỗi trong ModelState, quay lại trang đăng ký
            return View("Dangky");
        }

        public IActionResult Baiviet()
        {
            return View();
        }

        public IActionResult Chitiet()
        {
            return View();
        }

        public IActionResult Thongtincanhan()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
