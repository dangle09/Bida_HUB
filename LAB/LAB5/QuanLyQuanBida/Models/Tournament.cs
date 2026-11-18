using System;

namespace QuanLyQuanBida.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string TournamentName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal EntryFee { get; set; }
        public decimal TotalPrize { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public Tournament() { }
    }
}
