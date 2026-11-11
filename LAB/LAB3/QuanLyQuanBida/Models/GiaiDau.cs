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

        [Required]
        [StringLength(150)]
        [Column("ten_giai")]
        public string TenGiai { get; set; } = string.Empty;

        [Required]
        [Column("ngay_bat_dau")]
        public DateTime NgayBatDau { get; set; }

        [Required]
        [Column("ngay_ket_thuc")]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        [Column("le_phi_tham_gia", TypeName = "decimal(18,2)")]
        public decimal LePhiThamGia { get; set; }

        [Required]
        [Column("tong_giai_thuong", TypeName = "decimal(18,2)")]
        public decimal TongGiaiThuong { get; set; }

        [Required]
        [StringLength(30)]
        [Column("trang_thai")]
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GiaiDau() { }
    }
}
