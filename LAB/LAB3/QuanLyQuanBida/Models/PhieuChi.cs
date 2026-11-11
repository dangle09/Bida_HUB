using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("phieu_chi")]
    public class PhieuChi
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("loai_chi_phi_id")]
        public int LoaiChiPhiId { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [Column("so_tien", TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [Required]
        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        [Required]
        [StringLength(255)]
        [Column("muc_dich_chi")]
        public string MucDichChi { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuChi() { }
    }
}
