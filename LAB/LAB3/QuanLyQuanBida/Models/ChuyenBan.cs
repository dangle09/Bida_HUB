using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("chuyen_ban")]
    public class ChuyenBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("hoa_don_id")]
        public int HoaDonId { get; set; }

        [Required]
        [Column("tu_ban_id")]
        public int TuBanId { get; set; }

        [Required]
        [Column("den_ban_id")]
        public int DenBanId { get; set; }

        [Required]
        [Column("gio_chuyen_ban")]
        public DateTime GioChuyenBan { get; set; }

        [StringLength(255)]
        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public ChuyenBan() { }
    }
}
