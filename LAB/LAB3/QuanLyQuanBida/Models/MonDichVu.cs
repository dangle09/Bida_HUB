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

        [Column("danh_muc_id")]
        public int DanhMucId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("don_vi_tinh")]
        public string DonViTinh { get; set; } = string.Empty;

        [Column("don_gia")]
        public decimal DonGia { get; set; }

        [Column("hinh_anh")]
        public string? HinhAnh { get; set; }

        [Column("dang_hoat_dong")]
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public MonDichVu() { }
    }
}
