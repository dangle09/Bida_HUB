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

        [Required]
        [StringLength(20)]
        [Column("so_tu")]
        public string SoTu { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("khu_vuc")]
        public string? KhuVuc { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        [Required]
        [Column("tien_thue_thang", TypeName = "decimal(18,2)")]
        public decimal TienThueThang { get; set; }

        // HÀM KHỞI TẠO
        public TuDo() { }
    }
}
