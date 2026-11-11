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

        [Required]
        [StringLength(50)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("phan_tram_giam", TypeName = "decimal(5,2)")]
        public decimal? PhanTramGiam { get; set; }

        [Column("so_tien_giam", TypeName = "decimal(18,2)")]
        public decimal? SoTienGiam { get; set; }

        [Required]
        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Required]
        [Column("ngay_het_han")]
        public DateTime NgayHetHan { get; set; }

        // HÀM KHỞI TẠO
        public KhuyenMai() { }
    }
}
