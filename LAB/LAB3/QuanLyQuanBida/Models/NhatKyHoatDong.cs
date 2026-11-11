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

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("hanh_dong")]
        public string HanhDong { get; set; } = string.Empty;

        [Column("bang_du_lieu")]
        public string? BangDuLieu { get; set; }

        [Column("ban_ghi_id")]
        public int? BanGhiId { get; set; }

        [Column("mo_ta")]
        public string? MoTa { get; set; }

        [Column("dia_chi_ip")]
        public string? DiaChiIp { get; set; }

        [Column("thoi_gian")]
        public DateTime ThoiGian { get; set; }

        // HÀM KHỞI TẠO
        public NhatKyHoatDong() { }
    }
}
