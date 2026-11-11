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

        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Column("loai_bao_tri")]
        public string LoaiBaoTri { get; set; } = string.Empty;

        [Column("chi_phi")]
        public decimal ChiPhi { get; set; }

        [Column("ngay_bao_tri")]
        public DateTime NgayBaoTri { get; set; }

        [Column("nguoi_thuc_hien")]
        public string? NguoiThucHien { get; set; }

        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public BaoTriBan() { }
    }
}
