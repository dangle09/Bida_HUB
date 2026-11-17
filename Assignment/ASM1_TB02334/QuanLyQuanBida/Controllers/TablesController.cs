using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN LÝ HÀNG HÓA - QUẢN LÝ BÀN & BIỂU PHÍ THEO KHUNG GIỜ
    // - Route: /Tables hoặc /Tables/Index
    // - Quản lý View: Views/Tables/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class TablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
