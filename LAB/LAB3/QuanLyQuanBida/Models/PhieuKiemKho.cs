using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("phieu_kiem_kho")]
    public class PhieuKiemKho
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [Column("ngay_kiem_ke")]
        public DateTime NgayKiemKe { get; set; }

        [StringLength(255)]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuKiemKho() { }
    }
}
