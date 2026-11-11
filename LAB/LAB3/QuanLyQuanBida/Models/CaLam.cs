using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("ca_lam")]
    public class CaLam
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Column("gio_bat_dau")]
        public TimeSpan GioBatDau { get; set; }

        [Required]
        [Column("gio_ket_thuc")]
        public TimeSpan GioKetThuc { get; set; }

        [Required]
        [Column("luong_theo_gio", TypeName = "decimal(18,2)")]
        public decimal LuongTheoGio { get; set; }

        // HÀM KHỞI TẠO
        public CaLam() { }
    }
}
