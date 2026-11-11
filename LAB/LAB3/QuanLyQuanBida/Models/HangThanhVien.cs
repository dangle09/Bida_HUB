using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("hang_thanh_vien")]
    public class HangThanhVien
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Column("diem_toi_thieu")]
        public int DiemToiThieu { get; set; }

        [Required]
        [Column("ti_le_giam_gia", TypeName = "decimal(5,2)")]
        public decimal TiLeGiamGia { get; set; }

        [StringLength(255)]
        [Column("mo_ta")]
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public HangThanhVien() { }
    }
}
