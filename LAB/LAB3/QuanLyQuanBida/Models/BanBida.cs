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

        [Column("khu_vuc_ban_id")]
        public int KhuVucBanId { get; set; }

        [Column("loai_ban_id")]
        public int LoaiBanId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public BanBida() { }
    }
}
