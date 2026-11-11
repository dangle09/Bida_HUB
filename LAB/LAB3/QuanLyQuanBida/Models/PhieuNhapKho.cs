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

        [Column("nha_cung_cap_id")]
        public int NhaCungCapId { get; set; }

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ngay_nhap")]
        public DateTime NgayNhap { get; set; }

        [Column("tong_tien_nhap")]
        public decimal TongTienNhap { get; set; }

        // HÀM KHỞI TẠO
        public PhieuNhapKho() { }
    }
}
