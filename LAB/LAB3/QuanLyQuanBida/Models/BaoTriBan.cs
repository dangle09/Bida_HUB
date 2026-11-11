using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("bao_tri_ban")]
    public class BaoTriBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("loai_bao_tri")]
        public string LoaiBaoTri { get; set; } = string.Empty;

        [Required]
        [Column("chi_phi", TypeName = "decimal(18,2)")]
        public decimal ChiPhi { get; set; }

        [Required]
        [Column("ngay_bao_tri")]
        public DateTime NgayBaoTri { get; set; }

        [StringLength(100)]
        [Column("nguoi_thuc_hien")]
        public string? NguoiThucHien { get; set; }

        [StringLength(255)]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public BaoTriBan() { }
    }
}
