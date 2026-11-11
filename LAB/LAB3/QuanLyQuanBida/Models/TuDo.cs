using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("tu_do")]
    public class TuDo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("so_tu")]
        public string SoTu { get; set; } = string.Empty;

        [Column("khu_vuc")]
        public string? KhuVuc { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        [Column("tien_thue_thang")]
        public decimal TienThueThang { get; set; }

        // HÀM KHỞI TẠO
        public TuDo() { }
    }
}
