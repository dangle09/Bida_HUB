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

        [Required]
        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("loai_giao_dich")]
        public string LoaiGiaoDich { get; set; } = string.Empty;

        [Required]
        [Column("so_diem")]
        public int SoDiem { get; set; }

        [Required]
        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        [StringLength(255)]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public GiaoDichDiem() { }
    }
}
