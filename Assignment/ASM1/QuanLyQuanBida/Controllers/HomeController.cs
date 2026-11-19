using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanBida.Models;

// ===================================================================================
// CONTROLLER CỦA WEB VÃNG LAI (DÀNH CHO KHÁCH HÀNG)
// - Route chính: http://localhost:PORT/ hoặc /Home/Index
// - Action Index() sẽ trả về View: Views/Home/Index.cshtml (dùng _CustomerLayout.cshtml)
// ===================================================================================
namespace QuanLyQuanBida.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "Không có quyền truy cập (403)";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
