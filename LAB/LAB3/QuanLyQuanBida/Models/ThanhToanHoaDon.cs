using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("thanh_toan_hoa_don")]
    public class ThanhToanHoaDon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("hoa_don_id")]
        public int HoaDonId { get; set; }

        [Column("phuong_thuc_id")]
        public int PhuongThucId { get; set; }

        [Column("so_tien")]
        public decimal SoTien { get; set; }

        [Column("thoi_gian_tt")]
        public DateTime ThoiGianTt { get; set; }

        // HÀM KHỞI TẠO
        public ThanhToanHoaDon() { }
    }
}
