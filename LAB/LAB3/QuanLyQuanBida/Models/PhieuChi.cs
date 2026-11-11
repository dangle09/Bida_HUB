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

        [Column("loai_chi_phi_id")]
        public int LoaiChiPhiId { get; set; }

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("so_tien")]
        public decimal SoTien { get; set; }

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        [Column("muc_dich_chi")]
        public string MucDichChi { get; set; } = string.Empty;

        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuChi() { }
    }
}
