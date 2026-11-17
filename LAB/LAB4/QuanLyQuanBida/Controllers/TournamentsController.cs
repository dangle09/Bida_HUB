using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: HỘI VIÊN & GIẢI ĐẤU - GIẢI ĐẤU GIAO LƯU & NHÁNH ĐẤU
    // - Route: /Tournaments hoặc /Tournaments/Index
    // - Quản lý View: Views/Tournaments/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class TournamentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
