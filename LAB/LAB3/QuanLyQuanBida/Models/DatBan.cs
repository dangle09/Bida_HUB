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

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("ban_bida_id")]
        public int BanBidaId { get; set; }

        [Column("thoi_gian_den")]
        public DateTime ThoiGianDen { get; set; }

        [Column("so_nguoi")]
        public int SoNguoi { get; set; }

        [Column("tien_coc")]
        public decimal TienCoc { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public DatBan() { }
    }
}
