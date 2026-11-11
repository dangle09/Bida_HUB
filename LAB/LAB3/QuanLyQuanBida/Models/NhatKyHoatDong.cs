using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("nhat_ky_hoat_dong")]
    public class NhatKyHoatDong
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("hanh_dong")]
        public string HanhDong { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("bang_du_lieu")]
        public string? BangDuLieu { get; set; }

        [Column("ban_ghi_id")]
        public int? BanGhiId { get; set; }

        [Column("mo_ta")]
        public string? MoTa { get; set; }

        [StringLength(45)]
        [Column("dia_chi_ip")]
        public string? DiaChiIp { get; set; }

        [Required]
        [Column("thoi_gian")]
        public DateTime ThoiGian { get; set; }

        // HÀM KHỞI TẠO
        public NhatKyHoatDong() { }
    }
}
