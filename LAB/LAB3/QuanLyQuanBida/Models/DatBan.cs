using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("dat_ban")]
    public class DatBan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Required]
        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Required]
        [Column("thoi_gian_den")]
        public DateTime ThoiGianDen { get; set; }

        [Required]
        [Column("so_nguoi")]
        public int SoNguoi { get; set; }

        [Required]
        [Column("tien_coc", TypeName = "decimal(18,2)")]
        public decimal TienCoc { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public DatBan() { }
    }
}
