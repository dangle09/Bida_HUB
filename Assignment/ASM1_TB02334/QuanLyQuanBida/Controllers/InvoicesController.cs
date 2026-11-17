using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: VẬN HÀNH & BÁN HÀNG - HÓA ĐƠN & THU NGÂN POS
    // - Route: /Invoices hoặc /Invoices/Index
    // - Quản lý View: Views/Invoices/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "ManagementAccess")]
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
