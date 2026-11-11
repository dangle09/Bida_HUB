using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("khach_hang")]
    public class KhachHang
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("hang_thanh_vien_id")]
        public int HangThanhVienId { get; set; }

        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Column("ho_ten")]
        public string HoTen { get; set; } = string.Empty;

        [Column("so_dien_thoai")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Column("diem_tich_luy")]
        public int DiemTichLuy { get; set; }

        [Column("account_balance")]
        public decimal AccountBalance { get; set; }

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public KhachHang() { }
    }
}
