using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN TRỊ & BÁO CÁO - NHÂN SỰ & PHÂN CA TRỰC
    // - Route: /Staff hoặc /Staff/Index
    // - Quản lý View: Views/Staff/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
