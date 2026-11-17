using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN TRỊ & BÁO CÁO - AI TRỢ LÝ BIDA
    // - Route: /AiAssistant hoặc /AiAssistant/Index
    // - Quản lý View: Views/AiAssistant/Index.cshtml
    // - Layout: Views/Shared/_AdminLayout.cshtml
    // ===================================================================================
    [Authorize]
    public class AiAssistantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
