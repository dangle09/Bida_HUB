using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("mon_dich_vu")]
    public class MonDichVu
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("danh_muc_id")]
        public int DanhMucId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("don_vi_tinh")]
        public string DonViTinh { get; set; } = string.Empty;

        [Required]
        [Column("don_gia", TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [StringLength(255)]
        [Column("hinh_anh")]
        public string? HinhAnh { get; set; }

        [Required]
        [Column("dang_hoat_dong")]
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public MonDichVu() { }
    }
}
