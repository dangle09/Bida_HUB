using System;

namespace QuanLyQuanBida.Models
{
    public class CoThuThamGia
    {
        public int Id { get; set; }
        public int GiaiDauId { get; set; }
        public int KhachHangId { get; set; }
        public int? SoHatGiong { get; set; }
        public DateTime NgayDangKy { get; set; }
        public int? ThuHangChungCuoc { get; set; }

        // HÀM KHỞI TẠO
        public CoThuThamGia() { }
    }
}
