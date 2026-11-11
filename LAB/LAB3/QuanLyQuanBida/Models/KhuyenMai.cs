using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("khuyen_mai")]
    public class KhuyenMai
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("phan_tram_giam")]
        public decimal? PhanTramGiam { get; set; }

        [Column("so_tien_giam")]
        public decimal? SoTienGiam { get; set; }

        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Column("ngay_het_han")]
        public DateTime NgayHetHan { get; set; }

        // HÀM KHỞI TẠO
        public KhuyenMai() { }
    }
}
