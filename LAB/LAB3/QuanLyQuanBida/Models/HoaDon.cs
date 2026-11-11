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

        [Required]
        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("khuyen_mai_id")]
        public int? KhuyenMaiId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_hoa_don")]
        public string MaHoaDon { get; set; } = string.Empty;

        [Required]
        [Column("gio_bat_dau")]
        public DateTime GioBatDau { get; set; }

        [Column("gio_ket_thuc")]
        public DateTime? GioKetThuc { get; set; }

        [Required]
        [Column("table_price", TypeName = "decimal(18,2)")]
        public decimal TablePrice { get; set; }

        [Required]
        [Column("service_price", TypeName = "decimal(18,2)")]
        public decimal ServicePrice { get; set; }

        [Required]
        [Column("tien_giam_gia", TypeName = "decimal(18,2)")]
        public decimal TienGiamGia { get; set; }

        [Required]
        [Column("tong_thanh_toan", TypeName = "decimal(18,2)")]
        public decimal TongThanhToan { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public HoaDon() { }
    }
}
