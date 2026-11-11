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

        [Required]
        [Column("hang_thanh_vien_id")]
        public int HangThanhVienId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_code")]
        public string MaCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("ho_ten")]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Phone]
        [Column("so_dien_thoai")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required]
        [Column("diem_tich_luy")]
        public int DiemTichLuy { get; set; }

        [Required]
        [Column("account_balance", TypeName = "decimal(18,2)")]
        public decimal AccountBalance { get; set; }

        [Required]
        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public KhachHang() { }
    }
}
