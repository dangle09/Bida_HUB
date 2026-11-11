using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("thiet_bi_ban")]
    public class ThietBiBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThietBiBan() { }
    }
}
