using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("gia_ban_khung_gio")]
    public class GiaBanKhungGio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("loai_ban_id")]
        public int LoaiBanId { get; set; }

        [Required]
        [Column("gio_bat_dau_kg")]
        public TimeSpan GioBatDauKg { get; set; }

        [Required]
        [Column("gio_ket_thuc_kg")]
        public TimeSpan GioKetThucKg { get; set; }

        [Required]
        [Column("gia_khung_gio", TypeName = "decimal(18,2)")]
        public decimal GiaKhungGio { get; set; }

        // HÀM KHỞI TẠO
        public GiaBanKhungGio() { }
    }
}
