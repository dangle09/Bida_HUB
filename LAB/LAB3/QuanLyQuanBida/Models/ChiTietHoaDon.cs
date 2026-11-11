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

        [Column("hoa_don_id")]
        public int HoaDonId { get; set; }

        [Column("mon_dich_vu_id")]
        public int MonDichVuId { get; set; }

        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Column("don_gia")]
        public decimal DonGia { get; set; }

        [Column("thanh_tien")]
        public decimal ThanhTien { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietHoaDon() { }
    }
}
