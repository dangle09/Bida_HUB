using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("lich_lam_viec")]
    public class LichLamViec
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("ca_lam_id")]
        public int CaLamId { get; set; }

        [Column("ngay_lam_viec")]
        public DateTime NgayLamViec { get; set; }

        [Column("ghi_chu")]
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public LichLamViec() { }
    }
}
