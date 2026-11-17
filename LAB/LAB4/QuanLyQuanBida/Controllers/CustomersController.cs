using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: HỘI VIÊN & GIẢI ĐẤU - KHÁCH HÀNG & VIP (TỦ ĐỒ, CƠ GỬI)
    // - Route: /Customers hoặc /Customers/Index
    // - Quản lý View: Views/Customers/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize(Policy = "ManagementAccess")]
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
