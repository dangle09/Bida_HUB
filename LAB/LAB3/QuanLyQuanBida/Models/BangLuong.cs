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

        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("thang_luong")]
        public int ThangLuong { get; set; }

        [Column("nam_luong")]
        public int NamLuong { get; set; }

        [Column("tong_gio_lam")]
        public decimal TongGioLam { get; set; }

        [Column("luong_co_ban")]
        public decimal LuongCoBan { get; set; }

        [Column("phu_cap")]
        public decimal PhuCap { get; set; }

        [Column("tien_thuong")]
        public decimal TienThuong { get; set; }

        [Column("tien_phat")]
        public decimal TienPhat { get; set; }

        [Column("thuc_linh")]
        public decimal ThucLinh { get; set; }

        // HÀM KHỞI TẠO
        public BangLuong() { }
    }
}
