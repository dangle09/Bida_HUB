using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: VẬN HÀNH & BÁN HÀNG - SƠ ĐỒ BÀN (POS)
    // - Route: /Pos hoặc /Pos/Index
    // - Quản lý View: Views/Pos/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "ManagementAccess")]
    public class PosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
