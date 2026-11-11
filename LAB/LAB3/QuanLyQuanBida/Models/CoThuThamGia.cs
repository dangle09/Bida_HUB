using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("co_thu_tham_gia")]
    public class CoThuThamGia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("giai_dau_id")]
        public int GiaiDauId { get; set; }

        [Column("khach_hang_id")]
        public int KhachHangId { get; set; }

        [Column("so_hat_giong")]
        public int? SoHatGiong { get; set; }

        [Column("ngay_dang_ky")]
        public DateTime NgayDangKy { get; set; }

        [Column("thu_hang_chung_cuoc")]
        public int? ThuHangChungCuoc { get; set; }

        // HÀM KHỞI TẠO
        public CoThuThamGia() { }
    }
}
