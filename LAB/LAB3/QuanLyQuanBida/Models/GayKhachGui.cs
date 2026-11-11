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

        [Required]
        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("thuong_hieu_gay")]
        public string? ThuongHieuGay { get; set; }

        [StringLength(100)]
        [Column("loai_ngon_co")]
        public string? LoaiNgonCo { get; set; }

        [Required]
        [Column("ngay_gui")]
        public DateTime NgayGui { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GayKhachGui() { }
    }
}
