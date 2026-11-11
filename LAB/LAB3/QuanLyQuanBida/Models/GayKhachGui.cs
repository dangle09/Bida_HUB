using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("gay_khach_gui")]
    public class GayKhachGui
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("thuong_hieu_gay")]
        public string? ThuongHieuGay { get; set; }

        [Column("loai_ngon_co")]
        public string? LoaiNgonCo { get; set; }

        [Column("ngay_gui")]
        public DateTime NgayGui { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GayKhachGui() { }
    }
}
