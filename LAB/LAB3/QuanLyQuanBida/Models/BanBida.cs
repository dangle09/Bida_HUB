using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("ban_bida")]
    public class BanBida
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("khu_vuc_ban_id")]
        public int KhuVucBanId { get; set; }

        [Required]
        [Column("loai_ban_id")]
        public int LoaiBanId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public BanBida() { }
    }
}
