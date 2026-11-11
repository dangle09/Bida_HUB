using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("bang_luong")]
    public class BangLuong
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Required]
        [Column("thang_luong")]
        public int ThangLuong { get; set; }

        [Required]
        [Column("nam_luong")]
        public int NamLuong { get; set; }

        [Required]
        [Column("tong_gio_lam", TypeName = "decimal(6,2)")]
        public decimal TongGioLam { get; set; }

        [Required]
        [Column("luong_co_ban", TypeName = "decimal(18,2)")]
        public decimal LuongCoBan { get; set; }

        [Required]
        [Column("phu_cap", TypeName = "decimal(18,2)")]
        public decimal PhuCap { get; set; }

        [Required]
        [Column("tien_thuong", TypeName = "decimal(18,2)")]
        public decimal TienThuong { get; set; }

        [Required]
        [Column("tien_phat", TypeName = "decimal(18,2)")]
        public decimal TienPhat { get; set; }

        [Required]
        [Column("thuc_linh", TypeName = "decimal(18,2)")]
        public decimal ThucLinh { get; set; }

        // HÀM KHỞI TẠO
        public BangLuong() { }
    }
}
