using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("hoa_don")]
    public class HoaDon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("khuyen_mai_id")]
        public int? KhuyenMaiId { get; set; }

        [Column("ma_hoa_don")]
        public string MaHoaDon { get; set; } = string.Empty;

        [Column("gio_bat_dau")]
        public DateTime GioBatDau { get; set; }

        [Column("gio_ket_thuc")]
        public DateTime? GioKetThuc { get; set; }

        [Column("table_price")]
        public decimal TablePrice { get; set; }

        [Column("service_price")]
        public decimal ServicePrice { get; set; }

        [Column("tien_giam_gia")]
        public decimal TienGiamGia { get; set; }

        [Column("tong_thanh_toan")]
        public decimal TongThanhToan { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public HoaDon() { }
    }
}
