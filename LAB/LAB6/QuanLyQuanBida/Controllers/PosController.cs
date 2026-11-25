using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: VẬN HÀNH & BÁN HÀNG - SƠ ĐỒ BÀN (POS)
    // - Giữ nguyên giao diện 20 bàn POS chuyên nghiệp, đồng bộ biểu phí 2 khung giờ tự động từ SQL Server
    // ===================================================================================
    [Authorize(Policy = "ManagementAccess")]
    public class PosController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public PosController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Sơ đồ bàn (POS) - BOLT BILLIARDS";

            var types = await _context.TableTypes.AsNoTracking().OrderBy(t => t.Id).Take(3).ToListAsync();
            var slots = await _context.TimeSlotPrices.AsNoTracking().OrderBy(s => s.TableTypeId).ThenBy(s => s.Id).ToListAsync();

            var slotConfig = types.Select((t, idx) =>
            {
                var tSlots = slots.Where(s => s.TableTypeId == t.Id).OrderBy(s => s.Id).ToList();
                var s1 = tSlots.Count >= 1 ? tSlots[0] : null;
                var s2 = tSlots.Count >= 2 ? tSlots[1] : s1;
                string key = idx == 0 ? "pool" : idx == 1 ? "carom" : "vip";

                return new
                {
                    key,
                    name = t.Name,
                    slot1StartMin = (int)(s1?.SlotStartTime.TotalMinutes ?? 480),
                    slot1EndMin = (int)(s1?.SlotEndTime.TotalMinutes ?? 1020),
                    slot1Price = (int)(s1?.SlotPrice ?? t.BasePrice),
                    slot2StartMin = (int)(s2?.SlotStartTime.TotalMinutes ?? 1020),
                    slot2EndMin = (int)(s2?.SlotEndTime.TotalMinutes ?? 480),
                    slot2Price = (int)(s2?.SlotPrice ?? t.BasePrice)
                };
            }).ToList();

            bool canSeeRevenue = User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("Cashier");
            var today = DateTime.Today;
            decimal todayRev = canSeeRevenue
                ? await _context.Invoices.AsNoTracking()
                    .Where(i => i.Status == "Đã thanh toán" && i.StartTime >= today)
                    .SumAsync(i => (decimal?)i.TotalPayment) ?? 0m
                : 0m;

            ViewBag.TimeSlotsJson = JsonSerializer.Serialize(slotConfig);
            ViewBag.TodayRevenue = todayRev;
            return View();
        }
    }
}
