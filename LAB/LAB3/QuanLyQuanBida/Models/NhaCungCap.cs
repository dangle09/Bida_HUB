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

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("so_dien_thoai")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Column("dia_chi")]
        public string? DiaChi { get; set; }

        // HÀM KHỞI TẠO
        public NhaCungCap() { }
    }
}
