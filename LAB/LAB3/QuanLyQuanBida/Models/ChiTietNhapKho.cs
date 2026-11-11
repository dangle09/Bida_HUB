using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("chi_tiet_nhap_kho")]
    public class ChiTietNhapKho
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("phieu_nhap_id")]
        public int PhieuNhapId { get; set; }

        [Column("mon_dich_vu_id")]
        public int MonDichVuId { get; set; }

        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Column("gia_nhap")]
        public decimal GiaNhap { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietNhapKho() { }
    }
}
