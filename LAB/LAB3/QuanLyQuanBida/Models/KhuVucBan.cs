using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("khu_vuc_ban")]
    public class KhuVucBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public KhuVucBan() { }
    }
}
