using System;

namespace QuanLyQuanBida.DAL
{
    public class LoaiChiPhiDAL
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public LoaiChiPhiDAL() { }
    }
}
