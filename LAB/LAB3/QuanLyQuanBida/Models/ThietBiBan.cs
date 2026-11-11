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

        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThietBiBan() { }
    }
}
