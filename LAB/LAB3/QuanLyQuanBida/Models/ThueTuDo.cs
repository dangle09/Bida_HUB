using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("thue_tu_do")]
    public class ThueTuDo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("tu_do_id")]
        public int TuDoId { get; set; }

        [Required]
        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Required]
        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Required]
        [Column("ngay_het_han")]
        public DateTime NgayHetHan { get; set; }

        [Required]
        [Column("tien_thue_thang", TypeName = "decimal(18,2)")]
        public decimal TienThueThang { get; set; }

        [Required]
        [Column("tien_coc", TypeName = "decimal(18,2)")]
        public decimal TienCoc { get; set; }

        [Required]
        [StringLength(20)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThueTuDo() { }
    }
}
