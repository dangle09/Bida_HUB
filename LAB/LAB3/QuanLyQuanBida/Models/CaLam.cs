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

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("gio_bat_dau")]
        public TimeSpan GioBatDau { get; set; }

        [Column("gio_ket_thuc")]
        public TimeSpan GioKetThuc { get; set; }

        [Column("luong_theo_gio")]
        public decimal LuongTheoGio { get; set; }

        // HÀM KHỞI TẠO
        public CaLam() { }
    }
}
