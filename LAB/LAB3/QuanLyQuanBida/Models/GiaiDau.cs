using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("giai_dau")]
    public class GiaiDau
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ten_giai")]
        public string TenGiai { get; set; } = string.Empty;

        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Column("ngay_ket_thuc")]
        public DateTime NgayKetThuc { get; set; }

        [Column("le_phi_tham_gia")]
        public decimal LePhiThamGia { get; set; }

        [Column("tong_giai_thuong")]
        public decimal TongGiaiThuong { get; set; }

        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GiaiDau() { }
    }
}
