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

        [Column("loai_ban_id")]
        public int LoaiBanId { get; set; }

        [Column("gio_bat_dau_kg")]
        public TimeSpan GioBatDauKg { get; set; }

        [Column("gio_ket_thuc_kg")]
        public TimeSpan GioKetThucKg { get; set; }

        [Column("gia_khung_gio")]
        public decimal GiaKhungGio { get; set; }

        // HÀM KHỞI TẠO
        public GiaBanKhungGio() { }
    }
}
