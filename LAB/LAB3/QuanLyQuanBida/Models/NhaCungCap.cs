using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("nha_cung_cap")]
    public class NhaCungCap
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Phone]
        [Column("so_dien_thoai")]
        public string SoDienThoai { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("dia_chi")]
        public string? DiaChi { get; set; }

        // HÀM KHỞI TẠO
        public NhaCungCap() { }
    }
}
