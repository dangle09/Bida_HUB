using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: VẬN HÀNH & BÁN HÀNG - THỰC ĐƠN & DỊCH VỤ F&B
    // - Route: /Menu hoặc /Menu/Index
    // - Quản lý View: Views/Menu/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "ManagementAccess")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
