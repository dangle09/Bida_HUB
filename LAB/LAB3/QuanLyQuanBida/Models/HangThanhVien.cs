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

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("diem_toi_thieu")]
        public int DiemToiThieu { get; set; }

        [Column("ti_le_giam_gia")]
        public decimal TiLeGiamGia { get; set; }

        [Column("mo_ta")]
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public HangThanhVien() { }
    }
}
