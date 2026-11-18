using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using QuanLyQuanBida.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN LÝ HÀNG HÓA - QUẢN LÝ 20 BÀN BIDA & BIỂU PHÍ 2 KHUNG GIỜ
    // - Đồng bộ 100% SQL Server: BilliardTables (20 bàn), TimeSlotPrices (2 khung/loại bàn), TableEquipments (20 bàn)
    // - Khi chỉnh sửa giờ hoặc giá ở đây -> Sơ đồ bàn POS tự động tính tiền theo khung giờ mới!
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class TablesController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public TablesController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? areaId)
        {
            ViewData["Title"] = "Quản lý Bàn & Giá - BOLT BILLIARDS";

            var areas = await _context.TableAreas.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
            var areaDict = areas.ToDictionary(a => a.Id, a => a);
            var types = await _context.TableTypes.AsNoTracking().OrderBy(t => t.Id).Take(3).ToListAsync();
            var typeDict = types.ToDictionary(t => t.Id, t => t);
            var tables = await _context.BilliardTables.AsNoTracking().OrderBy(t => t.TableAreaId).ThenBy(t => t.Code).ToListAsync();
            var slots = await _context.TimeSlotPrices.AsNoTracking().OrderBy(s => s.TableTypeId).ThenBy(s => s.Id).ToListAsync();
            var equipments = await _context.TableEquipments.AsNoTracking().OrderBy(e => e.BilliardTableId).ToListAsync();

            var slotsByType = slots.GroupBy(s => s.TableTypeId).ToDictionary(g => g.Key, g => g.OrderBy(x => x.Id).ToList());

            var rows = tables.Select(t =>
            {
                var typeSlots = slotsByType.ContainsKey(t.TableTypeId) ? slotsByType[t.TableTypeId] : new List<TimeSlotPrice>();
                decimal dayPrice = typeSlots.Count >= 1 ? typeSlots[0].SlotPrice : (typeDict.ContainsKey(t.TableTypeId) ? typeDict[t.TableTypeId].BasePrice : 59000m);
                return new PosTableCardViewModel
                {
                    Id = t.Id,
                    Code = t.Code,
                    Name = t.Name,
                    AreaId = t.TableAreaId,
                    AreaCode = areaDict.ContainsKey(t.TableAreaId) ? areaDict[t.TableAreaId].Code : "",
                    AreaName = areaDict.ContainsKey(t.TableAreaId) ? areaDict[t.TableAreaId].Name : "Khu vực",
                    TableTypeName = typeDict.ContainsKey(t.TableTypeId) ? typeDict[t.TableTypeId].Name : "Bàn Pool 9-Ball",
                    HourlyRate = dayPrice,
                    Status = t.Status
                };
            }).ToList();

            var vm = new TablesManageViewModel
            {
                Tables = areaId.HasValue && areaId.Value > 0 ? rows.Where(r => r.AreaId == areaId.Value).ToList() : rows,
                Areas = areas,
                TableTypes = types,
                TimeSlots = slots.Select(s => new TimeSlotRowViewModel
                {
                    Id = s.Id,
                    TableTypeName = typeDict.ContainsKey(s.TableTypeId) ? typeDict[s.TableTypeId].Name : $"Loại #{s.TableTypeId}",
                    SlotStartTime = s.SlotStartTime,
                    SlotEndTime = s.SlotEndTime,
                    SlotPrice = s.SlotPrice
                }).ToList(),
                Equipments = equipments,
                TableNameDict = tables.ToDictionary(t => t.Id, t => $"{t.Code} — {t.Name}")
            };

            ViewBag.AllTables = rows;
            ViewBag.SelectedAreaId = areaId;
            ViewBag.SlotsByType = slotsByType;
            ViewBag.TablesById = tables.ToDictionary(t => t.Id, t => t);
            return View(vm);
        }

        // POST: /Tables/UpdateTimeSlot — Sửa khung giờ & đơn giá cho Khung 1 / Khung 2 (lưu thẳng SQL Server, đồng bộ ngay sang POS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTimeSlot(int id, string startTime, string endTime, decimal slotPrice)
        {
            var slot = await _context.TimeSlotPrices.FindAsync(id);
            if (slot == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy khung giờ cần cập nhật.";
                return RedirectToAction(nameof(Index));
            }

            if (!TimeSpan.TryParse(startTime, out var tsStart) || !TimeSpan.TryParse(endTime, out var tsEnd) || slotPrice < 0)
            {
                TempData["ErrorMessage"] = "Giờ bắt đầu, giờ kết thúc hoặc đơn giá không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            slot.SlotStartTime = tsStart;
            slot.SlotEndTime = tsEnd;
            slot.SlotPrice = slotPrice;

            // Nếu là khung đầu tiên của loại bàn, đồng bộ BasePrice cho TableType
            var firstSlot = await _context.TimeSlotPrices
                .Where(s => s.TableTypeId == slot.TableTypeId)
                .OrderBy(s => s.Id)
                .FirstOrDefaultAsync();
            if (firstSlot != null && firstSlot.Id == slot.Id)
            {
                var tt = await _context.TableTypes.FindAsync(slot.TableTypeId);
                if (tt != null) tt.BasePrice = slotPrice;
            }

            await _context.SaveChangesAsync();
            var tType = await _context.TableTypes.FindAsync(slot.TableTypeId);
            TempData["SuccessMessage"] = $"Đã cập nhật khung giờ {tsStart:hh\\:mm} – {tsEnd:hh\\:mm} ({slotPrice:N0} đ/giờ) cho {tType?.Name}. Sơ đồ bàn POS đã tự động áp dụng biểu phí mới!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTable(string code, string name, int tableAreaId, int tableTypeId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ mã bàn và tên bàn.";
                return RedirectToAction(nameof(Index));
            }

            var normCode = code.Trim().ToUpper();
            if (await _context.BilliardTables.AnyAsync(t => t.Code == normCode))
            {
                TempData["ErrorMessage"] = $"Mã bàn '{normCode}' đã tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            var newTable = new BilliardTable
            {
                Code = normCode,
                Name = name.Trim(),
                TableAreaId = tableAreaId,
                TableTypeId = tableTypeId,
                Status = "Trống"
            };
            _context.BilliardTables.Add(newTable);
            await _context.SaveChangesAsync();

            _context.TableEquipments.Add(new TableEquipment
            {
                BilliardTableId = newTable.Id,
                Name = "Bộ bi Aramith Pro Cup + Đèn LED 3 chao + Cơ CLB tiêu chuẩn",
                Quantity = 1,
                Status = "Hoạt động tốt"
            });
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã thêm bàn mới '{newTable.Name}' ({normCode}) và thiết bị đi kèm.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var table = await _context.BilliardTables.FindAsync(id);
            if (table != null)
            {
                table.Status = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã chuyển trạng thái '{table.Name}' sang '{status}'.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
