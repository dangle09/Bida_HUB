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
    // CONTROLLER: HỘI VIÊN & GIẢI ĐẤU - KHÁCH HÀNG VIP, GỬI CƠ & 200 TỦ ĐỒ (PHÂN TRANG 30/TRANG)
    // ===================================================================================
    [Authorize(Policy = "CashierOrAbove")]
    public class CustomersController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;
        private const int DefaultPageSize = 30;

        public CustomersController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? searchString,
            int? tierId,
            int page = 1,
            int lockerPage = 1,
            string? lockerSearch = null,
            string? lockerStatus = null,
            string tab = "hoivien")
        {
            ViewData["Title"] = "Khách hàng & VIP - BOLT BILLIARDS";

            var tiers = await _context.MembershipTiers.AsNoTracking().OrderBy(t => t.MinPoints).ToListAsync();
            var tierDict = tiers.ToDictionary(t => t.Id, t => t);

            var customers = await _context.Customers.AsNoTracking().OrderByDescending(c => c.AccumulatedPoints).ThenBy(c => c.Id).ToListAsync();
            var invoices = await _context.Invoices.AsNoTracking().Where(i => i.CustomerId > 0 && i.Status == "Đã thanh toán").ToListAsync();
            var spendByCustomer = invoices.GroupBy(i => i.CustomerId)
                .ToDictionary(g => g.Key, g => new { Total = g.Sum(x => x.TotalPayment), Count = g.Count() });

            var allMemberRows = customers.Select(c =>
            {
                var tier = tierDict.ContainsKey(c.MembershipTierId) ? tierDict[c.MembershipTierId] : null;
                decimal spent = spendByCustomer.ContainsKey(c.Id) ? spendByCustomer[c.Id].Total : 0m;
                int invCount = spendByCustomer.ContainsKey(c.Id) ? spendByCustomer[c.Id].Count : 0;

                return new CustomerRowViewModel
                {
                    Id = c.Id,
                    Code = c.Code,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    TierName = tier?.Name ?? "Hội viên Đồng",
                    DiscountRate = tier?.DiscountRate ?? 0,
                    AccumulatedPoints = c.AccumulatedPoints,
                    AccountBalance = c.AccountBalance,
                    TotalSpent = spent,
                    InvoiceCount = invCount,
                    CreatedAt = c.CreatedAt
                };
            }).ToList();

            var filteredMembers = allMemberRows.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var kw = searchString.Trim().ToLower();
                filteredMembers = filteredMembers.Where(c => c.FullName.ToLower().Contains(kw) || c.PhoneNumber.Contains(kw) || c.Code.ToLower().Contains(kw));
            }
            if (tierId.HasValue && tierId.Value > 0 && tierDict.ContainsKey(tierId.Value))
            {
                string tName = tierDict[tierId.Value].Name;
                filteredMembers = filteredMembers.Where(c => c.TierName == tName);
            }

            var filteredMemberList = filteredMembers.ToList();
            int memberTotalPages = Math.Max(1, (int)Math.Ceiling(filteredMemberList.Count / (double)DefaultPageSize));
            page = Math.Clamp(page, 1, memberTotalPages);
            var pagedMembers = filteredMemberList.Skip((page - 1) * DefaultPageSize).Take(DefaultPageSize).ToList();

            // Tủ đồ cá nhân (200 tủ - phân trang 30 tủ / trang)
            var allLockers = await _context.Lockers.AsNoTracking().OrderBy(l => l.Id).ToListAsync();
            var filteredLockers = allLockers.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(lockerSearch))
            {
                var lkw = lockerSearch.Trim().ToLower();
                filteredLockers = filteredLockers.Where(l => l.LockerNumber.ToLower().Contains(lkw) || l.Area.ToLower().Contains(lkw));
            }
            if (!string.IsNullOrWhiteSpace(lockerStatus))
            {
                filteredLockers = filteredLockers.Where(l => l.Status == lockerStatus);
            }
            var filteredLockerList = filteredLockers.ToList();
            int lockerTotalPages = Math.Max(1, (int)Math.Ceiling(filteredLockerList.Count / (double)DefaultPageSize));
            lockerPage = Math.Clamp(lockerPage, 1, lockerTotalPages);
            var pagedLockers = filteredLockerList.Skip((lockerPage - 1) * DefaultPageSize).Take(DefaultPageSize).ToList();

            var cues = await _context.CustomerCues.AsNoTracking().OrderByDescending(c => c.ReceivedDate).ThenByDescending(c => c.Id).ToListAsync();

            var vm = new CustomersPageViewModel
            {
                Customers = pagedMembers,
                AllCustomerDropdown = allMemberRows.OrderBy(c => c.FullName).ToList(),
                Tiers = tiers,
                Cues = cues,
                CustomerNameDict = customers.ToDictionary(c => c.Id, c => c.FullName),
                Lockers = pagedLockers,
                AvailableLockers = allLockers.Where(l => l.Status == "Trống").Take(50).ToList(),
                DiamondCount = allMemberRows.Count(r => r.TierName.Contains("Kim Cương")),
                GoldCount = allMemberRows.Count(r => r.TierName.Contains("Vàng")),
                TotalPoints = allMemberRows.Sum(r => r.AccumulatedPoints),
                PageSize = DefaultPageSize,
                MemberPage = page,
                MemberTotalCount = filteredMemberList.Count,
                LockerPage = lockerPage,
                LockerTotalCount = filteredLockerList.Count,
                LockerRentedCount = allLockers.Count(l => l.Status != "Trống"),
                ActiveTab = tab
            };

            ViewBag.SearchString = searchString;
            ViewBag.SelectedTierId = tierId;
            ViewBag.LockerSearch = lockerSearch;
            ViewBag.LockerStatus = lockerStatus;
            ViewBag.TotalCustomers = allMemberRows.Count;
            ViewBag.TotalAllLockers = allLockers.Count;

            return View(vm);
        }

        // POST: /Customers/Create — Đăng ký hội viên mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string fullName, string phoneNumber, int membershipTierId, decimal accountBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ họ tên và số điện thoại hội viên.";
                return RedirectToAction(nameof(Index));
            }

            int nextNum = await _context.Customers.CountAsync() + 1;
            string code = $"KH-{nextNum:000}";
            while (await _context.Customers.AnyAsync(c => c.Code == code))
            {
                nextNum++;
                code = $"KH-{nextNum:000}";
            }

            _context.Customers.Add(new Customer
            {
                Code = code,
                FullName = fullName.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                MembershipTierId = membershipTierId,
                AccumulatedPoints = 20,
                AccountBalance = Math.Max(0, accountBalance),
                CreatedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã đăng ký thành công hội viên mới: {fullName.Trim()} ({code}).";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Customers/ReceiveCue — Nhận gửi cơ bida & thuê tủ đồ cá nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceiveCue(int customerId, string cueBrand, string shaftType, int? lockerId)
        {
            if (customerId <= 0 || string.IsNullOrWhiteSpace(cueBrand))
            {
                TempData["ErrorMessage"] = "Vui lòng chọn hội viên và nhập thương hiệu cơ cần gửi.";
                return RedirectToAction(nameof(Index), new { tab = "cogui" });
            }

            int nextNum = await _context.CustomerCues.CountAsync() + 1;
            string code = $"GAY_{nextNum:000}";
            while (await _context.CustomerCues.AnyAsync(c => c.Code == code))
            {
                nextNum++;
                code = $"GAY_{nextNum:000}";
            }

            _context.CustomerCues.Add(new CustomerCue
            {
                CustomerId = customerId,
                Code = code,
                CueBrand = cueBrand.Trim(),
                ShaftType = string.IsNullOrWhiteSpace(shaftType) ? "Ngọn tiêu chuẩn" : shaftType.Trim(),
                ReceivedDate = DateTime.Now,
                Status = "Đang lưu kho"
            });

            string lockerMsg = "";
            if (lockerId.HasValue && lockerId.Value > 0)
            {
                var locker = await _context.Lockers.FindAsync(lockerId.Value);
                if (locker != null && locker.Status == "Trống")
                {
                    locker.Status = "Đang thuê";
                    _context.LockerRentals.Add(new LockerRental
                    {
                        LockerId = locker.Id,
                        CustomerId = customerId,
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today.AddMonths(1),
                        MonthlyRent = locker.MonthlyRent,
                        Deposit = 100000m,
                        Status = "Đang thuê"
                    });
                    lockerMsg = $" và gán tủ {locker.LockerNumber}";
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã tiếp nhận gửi cơ '{cueBrand.Trim()}' (Mã {code}){lockerMsg} thành công!";
            return RedirectToAction(nameof(Index), new { tab = "cogui" });
        }

        // POST: /Customers/ReturnCue — Trả cơ cho khách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnCue(int id)
        {
            var cue = await _context.CustomerCues.FindAsync(id);
            if (cue != null)
            {
                cue.Status = cue.Status == "Đang lưu kho" ? "Đã trả khách" : "Đang lưu kho";
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái cơ {cue.Code} sang '{cue.Status}'.";
            }
            return RedirectToAction(nameof(Index), new { tab = "cogui" });
        }

        // POST: /Customers/ToggleLocker — Cho thuê nhanh / Trả tủ cá nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLocker(int id, int lockerPage = 1)
        {
            var locker = await _context.Lockers.FindAsync(id);
            if (locker != null)
            {
                locker.Status = locker.Status == "Trống" ? "Đang thuê" : "Trống";
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã chuyển trạng thái tủ {locker.LockerNumber} sang '{locker.Status}'.";
            }
            return RedirectToAction(nameof(Index), new { tab = "cogui", lockerPage });
        }
    }
}
