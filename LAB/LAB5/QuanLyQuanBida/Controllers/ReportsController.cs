using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN TRỊ & BÁO CÁO - PHÂN TÍCH KINH DOANH & DOANH THU
    // - Route: /Reports hoặc /Reports/Index
    // - Quản lý View: Views/Reports/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
