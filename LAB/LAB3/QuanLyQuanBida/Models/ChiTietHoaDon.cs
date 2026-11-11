using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("chi_tiet_hoa_don")]
    public class ChiTietHoaDon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("hoa_don_id")]
        public int HoaDonId { get; set; }

        [Required]
        [Column("mon_dich_vu_id")]
        public int MonDichVuId { get; set; }

        [Required]
        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Required]
        [Column("don_gia", TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [Required]
        [Column("thanh_tien", TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietHoaDon() { }
    }
}
