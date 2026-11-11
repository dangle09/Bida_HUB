using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("phieu_nhap_kho")]
    public class PhieuNhapKho
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nha_cung_cap_id")]
        public int NhaCungCapId { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [Column("ngay_nhap")]
        public DateTime NgayNhap { get; set; }

        [Required]
        [Column("tong_tien_nhap", TypeName = "decimal(18,2)")]
        public decimal TongTienNhap { get; set; }

        // HÀM KHỞI TẠO
        public PhieuNhapKho() { }
    }
}
