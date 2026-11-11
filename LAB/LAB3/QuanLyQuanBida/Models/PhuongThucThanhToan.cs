using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("phuong_thuc_thanh_toan")]
    public class PhuongThucThanhToan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Column("dang_hoat_dong")]
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public PhuongThucThanhToan() { }
    }
}
