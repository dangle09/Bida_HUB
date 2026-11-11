using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("giao_dich_diem")]
    public class GiaoDichDiem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("loai_giao_dich")]
        public string LoaiGiaoDich { get; set; } = string.Empty;

        [Column("so_diem")]
        public int SoDiem { get; set; }

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public GiaoDichDiem() { }
    }
}
