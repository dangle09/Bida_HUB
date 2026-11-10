using System;

namespace QuanLyQuanBida.DAL
{
    public class HoaDonDAL
    {
        public int Id { get; set; }
        public int BanBidaId { get; set; }
        public int NguoiDungId { get; set; }
        public int KhachHangId { get; set; }
        public int? KhuyenMaiId { get; set; }
        public string MaHoaDon { get; set; } = string.Empty;
        public DateTime GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public decimal TablePrice { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal TongThanhToan { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public HoaDonDAL() { }
    }
}
