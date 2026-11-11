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

        [Required]
        [Column("hoa_don_id")]
        public int HoaDonId { get; set; }

        [Required]
        [Column("phuong_thuc_id")]
        public int PhuongThucId { get; set; }

        [Required]
        [Column("so_tien", TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [Required]
        [Column("thoi_gian_tt")]
        public DateTime ThoiGianTt { get; set; }

        // HÀM KHỞI TẠO
        public ThanhToanHoaDon() { }
    }
}
